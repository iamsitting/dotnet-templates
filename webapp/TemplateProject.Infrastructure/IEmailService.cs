namespace TemplateProject.Infrastructure;

public interface IEmailService
{
    Task SendEmailGenericAsync(string toEmail, string toName, string subject, string message,
        string[]? ccEmails = null, CancellationToken cancellationToken = default);
}