using Outbox_Pattern.Services.Mails;

namespace Outbox_Pattern.Services.Workers;
public class SendEmailWorker : BackgroundService
{
    private readonly ILogger<SendEmailWorker> _logger;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(1));
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public SendEmailWorker(ILogger<SendEmailWorker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Send Email Worker started at: {time}", DateTimeOffset.UtcNow);
    
        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var sendEmailService = scope.ServiceProvider.GetRequiredService<SendEmailServiceFaced>();

                await sendEmailService.HandleSendEmailMessageAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the email.");
            }
    
        }
    
        _logger.LogInformation("SendEmailWorker stopping at: {time}", DateTimeOffset.Now);
    }
    
}