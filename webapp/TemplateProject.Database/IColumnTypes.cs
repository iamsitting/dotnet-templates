namespace TemplateProject.Database;

public interface IColumnTypes
{
    public string String(int n);
    public string Date();
    public string Timestamp();
    public string TimestampWithTimezone();
    public string SmallNumber();
}