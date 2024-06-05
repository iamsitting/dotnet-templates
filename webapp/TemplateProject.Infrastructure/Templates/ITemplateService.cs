namespace TemplateProject.Infrastructure.Templates;

public interface ITemplateService
{
    Task<string> GetTemplateFromParametersAsync<T>(T parameters,
        CancellationToken cancellationToken = default) where T : ITemplateParameters;

    string GetTemplateFromParameters<T>(T parameters) where T : ITemplateParameters;
}