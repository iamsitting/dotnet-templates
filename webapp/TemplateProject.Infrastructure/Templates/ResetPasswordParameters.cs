namespace TemplateProject.Infrastructure.Templates;

public class ResetPasswordParameters : ITemplateParameters
{
    private const string Path = "Templates/ResetPassword.html";
    public string FilePath { get; init; }

    private static class Params
    {
        public const string Url = "{{Url}}";
        public const string ImageUrl = "{{ImageUrl}}";
        public const string Subject = "{{Subject}}";
        public const string OptOutUrl = "{{OptOutUrl}}";
    }

    private readonly string _url;
    private readonly string _optOutUrl;
    private readonly string _imageUrl;
    private readonly string _subject;

    public ResetPasswordParameters(string url, string subject, string imageUrl, string optOutUrl)
    {
        _url = url;
        _optOutUrl = optOutUrl;
        _subject = subject;
        _imageUrl = imageUrl;
        FilePath = Path;
    }
    

    public string ParseTemplateString(string template)
    {
        var result = template.Replace(Params.Url, _url)
            .Replace(Params.ImageUrl, _imageUrl)
            .Replace(Params.Subject, _subject)
            .Replace(Params.OptOutUrl, _optOutUrl);
        return result;
    }
}