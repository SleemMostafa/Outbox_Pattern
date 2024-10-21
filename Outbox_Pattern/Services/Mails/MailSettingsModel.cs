using System.ComponentModel.DataAnnotations;

namespace Outbox_Pattern.Services.Mails;

public class MailSettingsModel
{
    [Required]
    public required string Email { get; init; }

    [Required]
    public required string DisplayName { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    public required string Host { get; init; }

    [Required]
    public required int Port { get; init; }
}