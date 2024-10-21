namespace Outbox_Pattern.Services.Mails;

public interface IMailService
{
    Task<bool> SendEmailAsync(string name,string email);
}