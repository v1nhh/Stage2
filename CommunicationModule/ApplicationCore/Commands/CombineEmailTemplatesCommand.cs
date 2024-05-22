using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using CTAM.Core;

namespace CommunicationModule.ApplicationCore.Commands
{
    public class CombineEmailTemplatesCommand : IRequest<string>
    {
        public string Body { get; set; }

        public int MarkupTemplateID { get; set; }

        public CombineEmailTemplatesCommand(string body, int markupTemplateID)
        {
            Body = body;
            MarkupTemplateID = markupTemplateID;
        }
    }

    public class CombineEmailTemplatesHandler : IRequestHandler<CombineEmailTemplatesCommand, string>
    {
        private readonly ILogger<CombineEmailTemplatesHandler> _logger;
        private MainDbContext _context;

        public CombineEmailTemplatesHandler(ILogger<CombineEmailTemplatesHandler> logger, MainDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<string> Handle(CombineEmailTemplatesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CombineEmailTemplatesHandler called");
            var bodyRegex = @"\{\{body\}\}";
            var bodyReplaceValue = "{{body}}";

            if (string.IsNullOrWhiteSpace(request.Body))
            {
                var msg = $"Email body cannot be empty!";
                _logger.LogError(msg);
                throw new ArgumentNullException(msg);
            }
            var markupTemplateRecord = await _context.MailMarkupTemplate().AsNoTracking().Where(mmt => mmt.ID == request.MarkupTemplateID).FirstOrDefaultAsync();
            if (markupTemplateRecord == null || string.IsNullOrWhiteSpace(markupTemplateRecord.Template))
            {
                var msg = $"Email markup template cannot be null or empty!";
                _logger.LogError(msg);
                throw new ArgumentNullException(msg);
            }
            var markupTemplate = markupTemplateRecord.Template;
            var expectedBodiesCount = Regex.Matches(markupTemplate, bodyRegex).Count;
            if (expectedBodiesCount != 1)
            {
                var msg = "Email should contain exactly one '{{body}}' element";
                _logger.LogError(msg);
                throw new ArgumentOutOfRangeException(msg);
            }
            var mailBody = markupTemplate.Replace(bodyReplaceValue, request.Body);
            _logger.LogInformation("Created new email from template");
            return mailBody;
        }
    }

}
