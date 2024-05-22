using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;

namespace TemplateProject.Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailOptions _options;
    private readonly bool _isEmailEnabled;
    public EmailService(IOptions<EmailOptions> options, IHostEnvironment environment)
    {
        _options = options.Value;
        _isEmailEnabled = !environment.IsDevelopment();
    }

    private async Task SendEmailAsync(MimeMessage mimeMessage, CancellationToken cancellationToken = default)
    {
        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.StartTls, cancellationToken);
        await smtpClient.SendAsync(mimeMessage, cancellationToken);
        await smtpClient.DisconnectAsync(true, cancellationToken);

    }

    public async Task SendEmailGenericAsync(string toEmail, string toName, string subject, string message, string[]? ccEmails = null, CancellationToken cancellationToken = default)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress(_options.FromName, _options.FromAddress));
        mailMessage.To.Add(_isEmailEnabled ?
            new MailboxAddress(toName, toEmail.Trim())
            : new MailboxAddress(_options.DevName, _options.DevEmailAddress));
        mailMessage.Subject = subject;
        var ccs = ccEmails ?? [];
        if (ccs.Length != 0 && _isEmailEnabled)
        {
            var addresses = ccs.Select(x => new MailboxAddress(x.TrimEnd(), x.Trim()));
            mailMessage.Cc.AddRange(addresses);
        }

        mailMessage.Body = new TextPart("html") { Text = message };
        await SendEmailAsync(mailMessage, cancellationToken);
    }
}