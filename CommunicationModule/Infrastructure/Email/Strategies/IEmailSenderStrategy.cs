using System.Threading.Tasks;
using CommunicationModule.Infrastructure.Email.Services;

namespace CommunicationModule.Infrastructure.Email.Strategies
{
    /// <summary>
    /// Defines a strategy for creating email sender instances.
    /// </summary>
    public interface IEmailSenderStrategy
    {
        /// <summary>
        /// Creates an email sender instance based on the given configuration asynchronously.
        /// </summary>
        /// <param name="config">The email sender configuration details.</param>
        /// <returns>A task that represents the asynchronous operation, resulting in an IEmailSender.</returns>
        Task<IEmailSender> CreateEmailSenderAsync(EmailSenderConfiguration config);
    }
}
