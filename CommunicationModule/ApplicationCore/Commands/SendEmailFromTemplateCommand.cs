using CommunicationModule.ApplicationCore.DataManagers;
using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CommunicationModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommunicationModule.ApplicationCore.Commands
{
    public class SendEmailFromTemplateCommand: IRequest<bool>
    {
        public string MailTemplateName { get; set; }

        public string LanguageCode { get; set; }

        public int MailMarkupTemplateID { get; set; }

        public string MailTo { get; set; }

        public string MailCC { get; set; }

        public bool Prio { get; set; }

        public string Reference { get; set; }

        public Dictionary<string, string> EmailValues { get; set; }
    }

    public class SendEmailFromTemplateHandler : IRequestHandler<SendEmailFromTemplateCommand, bool>
    {
        private readonly ILogger<SendEmailFromTemplateHandler> _logger;
        private CommunicationDataManager _communicationDataManager;

        public SendEmailFromTemplateHandler(ILogger<SendEmailFromTemplateHandler> logger, CommunicationDataManager communicationDataManager)
        {
            _logger = logger;
            _communicationDataManager = communicationDataManager;
        }

        public async Task<bool> Handle(SendEmailFromTemplateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SendEmailFromTemplateHandler called");
            var templateRecord = await _communicationDataManager.GetMailTemplateByName(request.MailTemplateName, request.LanguageCode);

            if(templateRecord.IsActive)
            {
                var mailSubject = templateRecord.Subject.FillTemplateWithDictionaryValues(request.MailTemplateName, request.EmailValues);
                var mailBody = templateRecord.Template.FillTemplateWithDictionaryValues(request.MailTemplateName, request.EmailValues);
                // All IDs begin with 1, if ID is not provided it will be 0 so should be set to a default template
                var markupTemplateID = request.MailMarkupTemplateID != 0 ? request.MailMarkupTemplateID : 1;
                _communicationDataManager.AddMailQueueEntry(
                    new MailQueue()
                    {
                        MailMarkupTemplateID = markupTemplateID,
                        MailTo = request.MailTo,
                        MailCC = request.MailCC,
                        Prio = request.Prio,
                        Subject = mailSubject,
                        Body = mailBody,
                        Reference = request.Reference,
                        Status = MailQueueStatus.Created
                    });
                await _communicationDataManager.SaveChangesAsync();
                _logger.LogInformation($"Added new email '{mailSubject}' with mailTo '{request.MailTo}' to the MailQueue");
                return true;
            }
            return false;
        }
    }
}
