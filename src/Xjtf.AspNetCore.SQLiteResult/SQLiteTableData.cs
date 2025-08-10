namespace Xjtf.AspNetCore.SQLiteResult;

public class SQLiteTableData
{
    public required string TableName { get; init; }
    public List<SQLiteRowData> Rows { get; init; } = [];

    public SQLiteRowData this[int index]
    {
        get
        {
            return Rows[index];
        }
    }
}
