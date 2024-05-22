using CloudAPI.ApplicationCore.DTO.Integration;
using CloudAPI.ApplicationCore.Interfaces;
using CommunicationModule.ApplicationCore.Constants;
using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Enums;
using UserRoleModule.ApplicationCore.Utilities;
using LogLevel = UserRoleModule.ApplicationCore.Enums.LogLevel;

namespace CloudAPI.Services
{
    public class RequestBackgroundService : BackgroundService
    {
        public IBackgroundRequestQueue _queue;
        private readonly ILogger<RequestBackgroundService> _logger;
        private RestClient _restClient;
        private readonly int _maxInitialRetry = 3;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        private readonly IConfiguration _config;

        public RequestBackgroundService(IBackgroundRequestQueue queue, ILogger<RequestBackgroundService> logger, IServiceProvider serviceProvider, IMediator mediator, ITenantService tenantService, IConfiguration config)
        {
            _queue = queue;
            _logger = logger;
            _restClient = new RestClient();
            _mediator = mediator;
            _tenantService = tenantService;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var requestTenantPair = await _queue.TryDequeueAsync(stoppingToken);
                try
                {
                    var dbConnection = _tenantService.GetConnectionStringForTenant(requestTenantPair.TenantID);
                    if(!string.IsNullOrEmpty(dbConnection))
                    {
                        using (var dbContext = _tenantService.GetDbContext(dbConnection))
                        {
                            var apiSetting = await dbContext.APISetting().Where(x => x.ID == requestTenantPair.Request.APISettingID).FirstOrDefaultAsync();
                            if (apiSetting != null)
                            {
                                if (apiSetting.HasAuthentication)
                                {
                                    switch (apiSetting.AuthenticationTriggerName)
                                    {
                                        case APITriggerName.OAuth2ClientCredentials:
                                            var authApiSetting = await dbContext.APISetting().Where(x => x.TriggerName.Equals(apiSetting.AuthenticationTriggerName)).FirstOrDefaultAsync();

                                            var authRequest = new RestRequest(new Uri(authApiSetting.API_URL), authApiSetting.CrudOperation);
                                            authRequest.AddHeaders(authApiSetting.API_HEADERS);
                                            foreach (var param in authApiSetting.API_BODY)
                                            {
                                                authRequest.AddParameter(param.Key, param.Value);
                                            }
                                            RestResponse authResponse = await _restClient.ExecuteAsync(authRequest);
                                            if (authResponse.IsSuccessStatusCode)
                                            {
                                                var authResponseDeserialized = JsonSerializer.Deserialize<OAuth2ClientCredentialsResponse>(authResponse.Content, new JsonSerializerOptions
                                                {
                                                    PropertyNameCaseInsensitive = true
                                                });

                                                var restRequest = new RestRequest(new Uri(apiSetting.API_URL), apiSetting.CrudOperation);
                                                restRequest.AddHeader("Authorization", $"Bearer {authResponseDeserialized.Access_token}");
                                                AddHeadersAndBody(restRequest, apiSetting.API_HEADERS, requestTenantPair.Request.RequestBody);
                                                await SendRequestAsync(dbContext, restRequest, apiSetting, requestTenantPair.Request, requestTenantPair.TenantID);
                                            }
                                            else
                                            {
                                                var requestFromDb = await dbContext.Request().Where(x => x.ID.Equals(requestTenantPair.Request.ID)).FirstOrDefaultAsync();
                                                requestFromDb.ResponseBody = $"Failed with authentication. Error: {authResponse.Content}";
                                                await dbContext.SaveChangesAsync();
                                            }
                                            break;
                                        default:
                                            throw new ArgumentException($"APISetting has authentication but does not match a valid authentication method.");
                                    }
                                }
                                // No authentication needed for request. Continue building and executing RestRequest.
                                else
                                {
                                    var restRequest = new RestRequest(new Uri(apiSetting.API_URL), apiSetting.CrudOperation);
                                    AddHeadersAndBody(restRequest, apiSetting.API_HEADERS, requestTenantPair.Request.RequestBody);
                                    await SendRequestAsync(dbContext, restRequest, apiSetting, requestTenantPair.Request, requestTenantPair.TenantID);
                                }
                                _logger.LogInformation($"Request handled with ID: {requestTenantPair.Request.ID} and trigger name {requestTenantPair.Request.APISetting.TriggerName}.");
                            }
                            else
                            {
                                throw new ArgumentException($"APISetting of request with ID: {requestTenantPair.Request.ID}, not found");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, $"RequestBackgroundService failed handling the request tenant pair with tenant: {requestTenantPair.TenantID} and request ID: {requestTenantPair.Request.ID}");
                }
            }
        }

        private void AddHeadersAndBody(RestRequest restRequest, Dictionary<string, string> headers, string requestBody)
        {
            foreach (var header in headers)
            {
                restRequest.AddHeader(header.Key, header.Value);
            }
            restRequest.AddJsonBody(requestBody);
        }


        private async Task SendRequestAsync(MainDbContext mainDbContext, RestRequest restRequest, APISetting apiSetting, Request request, string tenantID)
        {
            RestResponse restResponse = await _restClient.ExecuteAsync(restRequest);
            var requestFromDb = await mainDbContext.Request().Where(r => r.ID == request.ID).FirstOrDefaultAsync();
             if (restResponse.IsSuccessStatusCode)
            {
                requestFromDb.ResponseBody = restResponse.Content;
                Type type = Type.GetType(apiSetting.ResponseT);
                if (type == null || !typeof(GenericResponse).IsAssignableFrom(type))
                {
                    throw new ArgumentException("Could not map ResponseT class to valid Type");
                }
                var responseDeserialized = JsonSerializer.Deserialize(string.IsNullOrEmpty(restResponse.Content) ? "{}" : restResponse.Content,
                    type, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var response = (GenericResponse)responseDeserialized;
                response.TenantID = tenantID;
                response.RequestOrigin = request;
                // Optional post execution after a valid response is received from a request.
                await _mediator.Send(response);
            }
            else
            {
                var maxRetryCountFailedRequests = _config.GetValue<int>("MaxRetryCountFailedRequests", 10);
                requestFromDb.ResponseBody = restResponse.Content;
                if (requestFromDb.RetryCount < _maxInitialRetry)
                {
                    requestFromDb.RetryCount++;
                    await SendRequestAsync(mainDbContext, restRequest, apiSetting, request, tenantID);
                }
                else if(requestFromDb.RetryCount == _maxInitialRetry && requestFromDb.Status != RequestStatus.Failed)
                {
                    // Request is aborted. ManageRequestsService will pick it up later.
                    await AddManagementLogAsync(mainDbContext, LogLevel.Error, LogSource.CloudAPI, nameof(CloudTranslations.managementLog_requestFailedForInitialMaxRetry), new[] { ("id", requestFromDb.ID.ToString()) });
                    requestFromDb.Status = RequestStatus.Failed;
                }
                else if (requestFromDb.RetryCount >= _maxInitialRetry && requestFromDb.RetryCount < maxRetryCountFailedRequests)
                {
                    requestFromDb.RetryCount++;
                    if(requestFromDb.RetryCount == maxRetryCountFailedRequests)
                    {
                        await AddManagementLogAsync(mainDbContext, LogLevel.Error, LogSource.CloudAPI, nameof(CloudTranslations.managementLog_requestFailedForMaxRetry), 
                            new[] { ("id", requestFromDb.ID.ToString()), ("maxRetryCountFailedRequests", maxRetryCountFailedRequests.ToString()) });
                    }
                }
            }
            await mainDbContext.SaveChangesAsync();
        }

        private async Task AddManagementLogAsync(MainDbContext context, LogLevel level, LogSource source, string logResourcePath, (string key, string value)[] parameters)
        {
            var logItem = ManagementLogUtilities.CreateManagementLog(level, source, logResourcePath, parameters, null);
            await context.ManagementLog().AddAsync(logItem);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RequestBackgroundService is stopping.");
            await base.StopAsync(stoppingToken);
        }
    }
}