using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Outbox_Pattern.Data;
using Outbox_Pattern.Endpoints;

namespace Outbox_Pattern.Services.Mails;

public class SendEmailServiceFaced(Db db,IMailService mailService,ILogger<SendEmailServiceFaced> logger)
{
    public async Task HandleSendEmailMessageAsync(CancellationToken cancellationToken)
    {
        var messages = await db.OutboxMessages
            .Where(msg => msg.Type == $"event: {nameof(EmployeeCreatedRequest)}" && !msg.IsProcessed)
            .OrderBy(msg => msg.Created)
            .Take(10)
            .ToListAsync(cancellationToken);

        foreach (var message in messages)
        {
            var data = JsonSerializer.Deserialize<EmployeeCreatedRequest>(message.Data);

            await mailService.SendEmailAsync(data.Name, data.Email);

            message.MarkAsProcessed(DateTime.UtcNow);

            logger.LogInformation("Email sent to {email} at: {time}", data.Email, DateTime.UtcNow);
        }

        db.OutboxMessages.UpdateRange(messages);
        await db.SaveChangesAsync(cancellationToken);
    }
}