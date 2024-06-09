namespace CleanProject.Database;

internal class SqliteColumnTypes : IColumnTypes
{
    public string String(int n) => "TEXT";
    public string String() => "TEXT";

    public string Date() => "NUMERIC";

    public string Timestamp() => "NUMERIC";

    public string TimestampWithTimezone() => "NUMERIC";

    public string SmallNumber() => "INTEGER";
}