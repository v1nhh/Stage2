using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Exceptions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CommunicationModule.ApplicationCore.DataManagers
{
    public class CommunicationDataManager
    {
        private readonly ILogger<CommunicationDataManager> _logger;
        private readonly MainDbContext _context;

        public CommunicationDataManager(MainDbContext context, ILogger<CommunicationDataManager> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Entities.MailTemplate> GetMailTemplateByName(string mailTemplateName, string languageCode)
        {
            var templateRecord = await _context.MailTemplate().AsNoTracking()
                .Where(mt => mt.Name.Equals(mailTemplateName) && mt.LanguageCode.Equals(languageCode))
                .FirstOrDefaultAsync();

            if (templateRecord == null)
            {
                _logger.LogWarning($"MailTemplate with name '{mailTemplateName}' does not exist for language code '{languageCode}'!");
                // If no email template for chosen language exist get English one by default
                templateRecord = await _context.MailTemplate().AsNoTracking()
                    .Where(mt => mt.Name.Equals(mailTemplateName) && mt.LanguageCode.Equals(Language.EnglishUS.GetLanguageCode()))
                    .FirstOrDefaultAsync();
                if (templateRecord == null)
                {
                    // If there is no email template even in English it should throw an exception
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.communications_apiExceptions_mailTemplateNotFound,
                                                                    new Dictionary<string, string> { { "mailTemplateName", mailTemplateName },
                                                                                                     { "languageCode", languageCode },
                                                                                                     { "alternativeLanguageCode", Language.EnglishUS.GetLanguageCode() } });
                }
                else
                {
                    _logger.LogWarning($"Using '{Language.EnglishUS.GetLanguageCode()}' instead of '{languageCode}' for MailTemplate with name '{mailTemplateName}'!");
                }
            }

            return templateRecord;
        }

        public void AddMailQueueEntry(MailQueue mailQueue)
        {
            _context.MailQueue().Add(mailQueue);
        }

        public async Task AddMailQueueEntriesBulkAsync(List<MailQueue> mqs)
        {
            var bulkConfig = new BulkConfig();
            bulkConfig.BatchSize = 15000;
            await _context.BulkInsertAsync(mqs, bulkConfig);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
