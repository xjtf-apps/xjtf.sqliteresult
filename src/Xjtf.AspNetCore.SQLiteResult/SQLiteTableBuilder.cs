namespace Xjtf.AspNetCore.SQLiteResult;

public class SQLiteTableBuilder
{
    public string TableName { get; init; }
    public SQLiteResultBuilder Results { get; init; }
    public List<SQLiteColumnBuilder> ColumnBuilders { get; init; } = [];
    internal SQLiteTableBuilder(string tableName, SQLiteResultBuilder builder)
    {
        TableName = tableName;
        Results = builder;
    }
}
