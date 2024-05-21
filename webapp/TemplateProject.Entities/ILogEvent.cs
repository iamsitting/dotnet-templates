namespace TemplateProject.Entities;

public interface ILogEvent
{
    public string? Message { get; set; }
    public string? MessageTemplate { get; set; }
    public string? Level { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Exception { get; set; }
    public string? Properties { get; set; }
}