namespace CleanProject.CoreApplication.Infrastructure.Template;

public interface ITemplateParameters
{
    public string FilePath { get; init; }
    public string ParseTemplateString(string template);
}