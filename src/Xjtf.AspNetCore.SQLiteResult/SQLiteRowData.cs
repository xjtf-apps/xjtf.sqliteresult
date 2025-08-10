namespace Xjtf.AspNetCore.SQLiteResult;

public class SQLiteRowData(params object?[] fields)
{
    public List<object?> Fields { get; set; } = [.. fields];
}
