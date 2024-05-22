using CommunicationModule.ApplicationCore.Entities;
using System.Threading.Tasks;

namespace CommunicationModule.Infrastructure.Email.Strategies
{
    /// <summary>
    /// Interface for email sending services.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="mailQueue">Mail queue item containing email details.</param>
        /// <returns>A task representing the send operation.</returns>
        /// <exception cref="System.Exception">Exceptions are to be handled by the caller.</exception>
        Task SendEmailAsync(MailQueue mailQueue);

        /// <summary>
        /// Disconnects from the email service asynchronously.
        /// </summary>
        /// <returns>A task representing the disconnect operation.</returns>
        /// <exception cref="System.Exception">Exceptions are to be handled by the caller.</exception>
        Task DisconnectAsync();
    }
}
