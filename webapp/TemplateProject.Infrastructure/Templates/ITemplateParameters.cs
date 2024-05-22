namespace TemplateProject.Infrastructure.Templates;

public interface ITemplateParameters
{
    public string FilePath { get; init; }
    public string ParseTemplateString(string template);
}