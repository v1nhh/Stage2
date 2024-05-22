using CloudAPI.ApplicationCore.DTO.Integration;
using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CloudAPI.ApplicationCore.Commands
{
    /// <summary>
    /// Command passed to the typed IRequestHandler interface. Used by the mediator for object request handling.
    /// </summary>
    public class CreateRequestsCommand : IRequest
    {
        public string APITriggerName { get; set; }
        public object Context { get; set; }
        public string EntityType { get; set; }
        public int EntityID { get; set; }
        public int? ReferredRequestID { get; set; }
    }

    public class EnqueueRequestHandler : IRequestHandler<CreateRequestsCommand>
    {
        private readonly MainDbContext _mainDb;
        private readonly ILogger<EnqueueRequestHandler> _logger;

        public EnqueueRequestHandler(MainDbContext mainDb, ILogger<EnqueueRequestHandler> logger)
        {
            _mainDb = mainDb;
            _logger = logger;
        }

        /// <summary>
        /// Handle method responsible for creating Request entity and saving it to the database.
        /// </summary>
        /// <param name="command">Encapsulates the parameters to generate all Requests.</param>
        public async Task<Unit> Handle(CreateRequestsCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("EnqueueRequestHandler called");
            var requests = new List<Request>();
            var apiSettings = await _mainDb.APISetting().Where(s => s.IsActive && s.TriggerName.Equals(command.APITriggerName)).ToListAsync();
            foreach (APISetting apiSetting in apiSettings)
            {
                var requestToAdd = new Request()
                {
                    Status = RequestStatus.Created,
                    EntityType = command.EntityType,
                    EntityID = command.EntityID,
                    RequestBody = await generateRequestBody(command.Context, apiSetting.RequestT, apiSetting.API_BODY),
                    APISettingID = apiSetting.ID,
                };
                if (command.ReferredRequestID != 0) { requestToAdd.ReferredRequestID = command.ReferredRequestID; }
                requests.Add(requestToAdd);
            }
            if (requests.Any())
            {
                _mainDb.Request().AddRange(requests);
                await _mainDb.SaveChangesAsync();
            }
            return new Unit();
        }

        /// <summary>
        /// Generates the JSON body of the request, which will be added as content-type 'application/json' to the RestRequest.
        /// </summary>
        /// <param name="context">Can be any object. Extra type validations can be done for casting purposes in the concrete GenericRequest class.</param>
        /// <param name="requestT">RequestT specified in the APISetting record.</param>
        /// <returns>A JSON string.</returns>
        /// <exception cref="ArgumentException"></exception>
        private async Task<string> generateRequestBody(object context, string requestT, Dictionary<string, JsonElement> apiSettingJsonBody)
        {
            Type type = Type.GetType(requestT);
            if (type == null || !typeof(GenericRequest).IsAssignableFrom(type))
            {
                throw new ArgumentException("Could not map RequestT class to valid Type");
            }
            var request = (GenericRequest)Activator.CreateInstance(type);
            request.Context = context;
            request.MainDbContext = _mainDb;
            request.APISettingBody = apiSettingJsonBody;
            await request.CollectDataAsync();
            var requestJsonBody = request.GetJsonBody();
            return requestJsonBody;
        }
    }
}
