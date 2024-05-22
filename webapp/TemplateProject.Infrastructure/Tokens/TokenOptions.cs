namespace TemplateProject.Infrastructure.Tokens;

public class TokenOptions
{
    public const string Key = "TokenOptions";
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}