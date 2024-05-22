namespace TemplateProject.Infrastructure.Templates;

public interface ITemplateService
{
    Task<string> GetResetPasswordTemplate(ResetPasswordParameters parameters,
        CancellationToken cancellationToken = default);
}