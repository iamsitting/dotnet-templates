namespace TemplateProject.Database;

internal class SqliteColumnTypes : IColumnTypes
{
    public string String(int n) => "TEXT";

    public string Date() => "NUMERIC";

    public string Timestamp() => "NUMERIC";

    public string TimestampWithTimezone() => "NUMERIC";

    public string SmallNumber() => "INTEGER";
}