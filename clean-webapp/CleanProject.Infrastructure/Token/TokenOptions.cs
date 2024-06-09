namespace CleanProject.Infrastructure.Token;

internal class TokenOptions
{
    public const string Key = "TokenOptions";
    public string SecretKey { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
}