namespace Xjtf.AspNetCore.SQLiteResult;

public static class SQLiteBuilderExtensions
{
    public static SQLiteTableBuilder Table(this SQLiteResultBuilder builder, string tableName)
    {
        var tableBuilder = new SQLiteTableBuilder(tableName, builder);
        builder.TableData.Add(new SQLiteTableData() { TableName = tableBuilder.TableName });
        builder.TableBuilders.Add(tableBuilder);
        return tableBuilder;
    }

    public static SQLiteTableBuilder Column(this SQLiteTableBuilder builder, SQLiteColumnBuilder columnBuilder)
    {
        builder.ColumnBuilders.Add(columnBuilder);
        return builder;
    }

    public static SQLiteTableData Row(this SQLiteTableData table, SQLiteRowData row)
    {
        table.Rows.Add(row);
        return table;
    }

    public static SQLiteColumnBuilder Nullable(this SQLiteColumnBuilder builder)
    {
        builder.IsNullable = true;
        return builder;
    }

    public static SQLiteColumnBuilder NotNullable(this SQLiteColumnBuilder builder)
    {
        builder.IsNullable = false;
        return builder;
    }

    //public static SQLiteColumnBuilder IsIndexed(this SQLiteColumnBuilder builder)
    //{
    //    builder.IsIndexed = true;
    //    return builder;
    //}

    public static SQLiteColumnBuilder IsUnique(this SQLiteColumnBuilder builder)
    {
        builder.IsUnique = true;
        return builder;
    }

    public static SQLiteColumnBuilder IsPrimaryKey(this SQLiteColumnBuilder builder)
    {
        builder.IsPrimaryKey = true;
        return builder;
    }

    public static SQLiteColumnBuilder IsAutoIncrement(this SQLiteColumnBuilder builder)
    {
        builder.IsAutoIncrement = true;
        return builder;
    }

    public static SQLiteColumnBuilder WithName(this SQLiteColumnBuilder builder, string name)
    {
        builder.ColumnName = name;
        return builder;
    }

    public static SQLiteColumnBuilder WithType(this SQLiteColumnBuilder builder, Type type)
    {
        builder.ColumnType = type;
        return builder;
    }

    public static SQLiteColumnBuilder WithDefault(this SQLiteColumnBuilder builder, object? defaultValue)
    {
        builder.DefaultValue = defaultValue;
        return builder;
    }

    public static SQLiteColumnBuilder With(this SQLiteColumnBuilder builder, string name, Type type, object? defaultValue = null)
    {
        builder.ColumnName = name;
        builder.ColumnType = type;
        builder.DefaultValue = defaultValue;
        return builder;
    }
}
