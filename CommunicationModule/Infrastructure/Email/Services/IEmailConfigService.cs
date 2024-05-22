using System.Threading.Tasks;

namespace CommunicationModule.Infrastructure.Email.Services
{
    public interface IEmailConfigService
    {
        Task<EmailSenderConfiguration> GetEmailSenderConfigurationAsync(string tenantIdentifier);
    }
}
