using CTAM.Core.Security;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using CTAMSharedLibrary.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Commands.Logs;
using UserRoleModule.ApplicationCore.Interfaces;

namespace UserRoleModule.ApplicationCore.Services
{
    public class ManagementLogger : IManagementLogger
    {
        private const Enums.LogSource LOG_SOURCE = Enums.LogSource.CloudAPI;

        private readonly ILogger<ManagementLogger> _logger;
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationUtilities _authUtils;
        private readonly System.Resources.ResourceManager _resourceManager;
        private readonly CultureInfo _cultureInfo;

        public ManagementLogger(ILogger<ManagementLogger> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _authUtils = new AuthenticationUtilities(configuration);
            _resourceManager = CloudTranslations.ResourceManager;
            _cultureInfo = CultureInfo.DefaultThreadCurrentCulture;
        }

        public async Task LogFatal(string logResourcePath, params (string key, string value)[] parameters)
        {
            var resourceName = TranslationUtils.GetResourceName(logResourcePath);
            var logResource = _resourceManager.GetString(resourceName, _cultureInfo);
            var message = !string.IsNullOrEmpty(logResource) ? TranslationUtils.GetTranslation(logResource, parameters)
                : $"Could not find translation for logResourcePath: {logResourcePath} with parameters: {TranslationUtils.Serialize(parameters)}";

            // Is critical = fatal?
            _logger.LogCritical(message);

            var subValue = _authUtils.ExtractSubValueFromClaims(_httpContextAccessor.HttpContext);
            await _mediator.Send(new AddManagementLogCommand(Enums.LogLevel.Fatal, LOG_SOURCE, logResourcePath, parameters, subValue));
        }

        public async Task LogError(string logResourcePath, params (string key, string value)[] parameters)
        {
            var resourceName = TranslationUtils.GetResourceName(logResourcePath);
            var logResource = _resourceManager.GetString(resourceName, _cultureInfo);
            var message = !string.IsNullOrEmpty(logResource) ? TranslationUtils.GetTranslation(logResource, parameters)
                : $"Could not find translation for logResourcePath: {logResourcePath} with parameters: {TranslationUtils.Serialize(parameters)}";

            _logger.LogError(message);

            var subValue = _authUtils.ExtractSubValueFromClaims(_httpContextAccessor.HttpContext);
            await _mediator.Send(new AddManagementLogCommand(Enums.LogLevel.Error, LOG_SOURCE, logResourcePath, parameters, subValue));
        }

        public async Task LogError(Exception exception)
        {
            var mostInnerException = exception.GetMostInnerException();
            _logger.LogError(mostInnerException.Message);

            var subValue = _authUtils.ExtractSubValueFromClaims(_httpContextAccessor.HttpContext);
            await _mediator.Send(new AddManagementLogCommand(Enums.LogLevel.Error, LOG_SOURCE, mostInnerException.Message, null, subValue));
        }

        public async Task LogWarning(string logResourcePath, params (string key, string value)[] parameters)
        {
            var resourceName = TranslationUtils.GetResourceName(logResourcePath);
            var logResource = _resourceManager.GetString(resourceName, _cultureInfo);
            var message = !string.IsNullOrEmpty(logResource) ? TranslationUtils.GetTranslation(logResource, parameters)
                : $"Could not find translation for logResourcePath: {logResourcePath} with parameters: {TranslationUtils.Serialize(parameters)}";

            _logger.LogWarning(message);
            var subValue = _authUtils.ExtractSubValueFromClaims(_httpContextAccessor.HttpContext);
            await _mediator.Send(new AddManagementLogCommand(Enums.LogLevel.Warning, LOG_SOURCE, logResourcePath, parameters, subValue));
        }

        public async Task LogInfo(string logResourcePath, params (string key, string value)[] parameters)
        {
            var resourceName = TranslationUtils.GetResourceName(logResourcePath);
            var logResource = _resourceManager.GetString(resourceName, _cultureInfo);
            var message = !string.IsNullOrEmpty(logResource) ? TranslationUtils.GetTranslation(logResource, parameters)
                : $"Could not find translation for logResourcePath: {logResourcePath} with parameters: {TranslationUtils.Serialize(parameters)}";

            _logger.LogInformation(message);

            var subValue = "";
            if (_httpContextAccessor.HttpContext != null)
            {
                subValue = _authUtils.ExtractSubValueFromClaims(_httpContextAccessor.HttpContext);
                if (subValue == null)
                {
                    Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
                    var userEmail = reg.Match(message).Value;
                    if (!String.IsNullOrEmpty(userEmail))
                    {
                        subValue = userEmail;
                    }
                }
            }
            await _mediator.Send(new AddManagementLogCommand(Enums.LogLevel.Info, LOG_SOURCE, logResourcePath, parameters, subValue));
        }

        public async Task LogDebug(string logResourcePath, params (string key, string value)[] parameters)
        {
            var resourceName = TranslationUtils.GetResourceName(logResourcePath);
            var logResource = _resourceManager.GetString(resourceName, _cultureInfo);
            var message = !string.IsNullOrEmpty(logResource) ? TranslationUtils.GetTranslation(logResource, parameters)
                : $"Could not find translation for logResourcePath: {logResourcePath} with parameters: {TranslationUtils.Serialize(parameters)}";

            _logger.LogDebug(message);
            var subValue = _authUtils.ExtractSubValueFromClaims(_httpContextAccessor.HttpContext);
            await _mediator.Send(new AddManagementLogCommand(Enums.LogLevel.Debug, LOG_SOURCE, logResourcePath, parameters, subValue));
        }
    }
}
