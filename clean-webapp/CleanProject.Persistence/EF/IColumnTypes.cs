namespace CleanProject.Persistence.EF;

public interface IColumnTypes
{
    public string String(int n);
    public string String();
    public string Date();
    public string Timestamp();
    public string TimestampWithTimezone();
    public string SmallNumber();
}