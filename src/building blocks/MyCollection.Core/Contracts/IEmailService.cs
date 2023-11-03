using MyCollection.Core.Models;

namespace MyCollection.Core.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
