using Outbox_Pattern.Services.Mails;

namespace Outbox_Pattern;

public static class Registration
{
    public static void ConfigureEmailService(this IHostApplicationBuilder builder)
    {
        var emailSection = builder.Configuration.GetRequiredSection("MailSettings");

        builder.Services.AddOptions<MailSettingsModel>().Bind(emailSection).ValidateDataAnnotations().ValidateOnStart();

        var settings =
            emailSection.Get<MailSettingsModel>() ?? throw new InvalidOperationException("Cannot load MailSettings");

        builder
            .Services.AddFluentEmail(settings.Email)
            .AddSmtpSender(settings.Host, settings.Port, settings.Email, settings.Password)
            .AddRazorRenderer();
    }
}