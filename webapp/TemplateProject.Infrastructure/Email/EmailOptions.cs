namespace TemplateProject.Infrastructure.Email;

public class EmailOptions
{
    public const string Key = "EmailOptions";
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string FromAddress { get; set; } = null!;
    public string FromName { get; set; } = null!;
    public string DevEmailAddress { get; set; } = null!;
    public string DevName { get; set; } = null!;
}