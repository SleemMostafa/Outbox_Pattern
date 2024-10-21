using FluentEmail.Core;
using Outbox_Pattern.Data.Entities;

namespace Outbox_Pattern.Services.Mails;

public class MailService(IFluentEmail fluentEmail, ILogger<MailService> logger) : IMailService
{
    public async Task<bool> SendEmailAsync(string name,string email)
    {
        try
        {
            var sendResponse = await fluentEmail
                .To(email)
                .Subject("Create")
                .Body($"Welcome {name} in our outbox pattern")
                .SendAsync();

            if (!sendResponse.Successful)
            {
                logger.LogError("Email fail to send {ErrorMessages}", sendResponse.ErrorMessages);

                return false;
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to send email");

            return false;
        }

        return true;
    }
}