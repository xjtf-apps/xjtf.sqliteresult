namespace Xjtf.AspNetCore.SQLiteResult;

public class SQLiteResultBuilder
{
    public SQLiteResultBuilder() { }
    public SQLiteResult Build() => new(this);
    public List<SQLiteTableData> TableData { get; init; } = [];
    public List<SQLiteTableBuilder> TableBuilders { get; init; } = [];

    public SQLiteRowData this[string tableName, int rowNumber]
    {
        get
        {
            return this[tableName].Rows[rowNumber];
        }
    }

    public SQLiteTableData this[string tableName]
    {
        get
        {
            return TableData.First(t => t.TableName == tableName)!;
        }
    }
}
