namespace Xjtf.AspNetCore.SQLiteResult;

public class SQLiteColumnBuilder
{
    public string ColumnName { get; set; }
    public Type ColumnType { get; set; }
    public object? DefaultValue { get; set; }

    public bool IsPrimaryKey { get; set; } = false;
    public bool IsAutoIncrement { get; set; } = false;
    //public bool IsIndexed { get; set; } = false;
    public bool IsNullable { get; set; } = false;
    public bool IsUnique { get; set; } = false;
}
