using SQLite;
using System.Text;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Xjtf.AspNetCore.SQLiteResult;

public class SQLiteResult : IActionResult
{
    private readonly SQLiteResultBuilder _builder;

    public SQLiteResult(SQLiteResultBuilder resultBuilder)
    {
         _builder = resultBuilder;
    }

    private ReadOnlyMemory<byte> GetSQLiteDatabaseBuffer()
    {
        var id = Guid.NewGuid();
        var filename = $"{id}.db";
        var db = new SQLiteConnection(filename);
        try
        {
            foreach (var tableBuilder in _builder.TableBuilders)
            {
                var tableName = tableBuilder.TableName;
                var tableColumns = tableBuilder.ColumnBuilders;
                var tableCreationScript = GenerateCreateTableSql(tableName, tableColumns);

                db.Execute(tableCreationScript);
            }

            for (var i = 0; i < _builder.TableData.Count; i++)
            {
                var table = _builder.TableData[i];
                var tableMeta = _builder.TableBuilders[i];
                var tableName = _builder.TableBuilders[i].TableName;
                var columnNames = tableMeta.ColumnBuilders.Select(cb => cb.ColumnName).ToArray();

                for (var j = 0; j < table.Rows.Count; j++)
                {
                    var row = table.Rows[j];
                    var columnsInsertList = columnNames.Aggregate("", (l,r) =>  $"{l}, {r}").TrimStart(' ', ',');
                    var columnsInsertString = $"INSERT INTO {tableName} ({columnsInsertList})";
                    var columnsInsertValues = $"VALUES ({row.Fields.Select(PrintField).Aggregate("", (l, r) => $"{l}, {r}").Trim(',', ' ')})";

                    db.Execute($"{columnsInsertValues}{Environment.NewLine}{columnsInsertValues}");
                }
            }

            return File.ReadAllBytes(filename);
        }
        finally
        {
            File.Delete(filename);
        }
    }

    private static string GenerateCreateTableSql(string tableName, List<SQLiteColumnBuilder> columns)
    {
        var scriptBuilder = new StringBuilder();
        scriptBuilder.AppendLine($"CREATE TABLE {tableName} (");
        for (int i = 0; i < columns.Count; i++)
        {
            var column = columns[i];
            if (i == columns.Count - 1)
                scriptBuilder.AppendLine(GenerateColumnSql(column));
            else
                scriptBuilder.AppendLine($"{GenerateColumnSql(column)},");
        }
        scriptBuilder.AppendLine(")");
        return scriptBuilder.ToString();
    }

    private static string GenerateColumnSql(SQLiteColumnBuilder column)
    {
        var columnName = column.ColumnName;
        var columnUnique = column.IsUnique ? "unique" : "";
        var columnType = GenerateColumnType(column.ColumnType);
        var columnNullability = column.IsPrimaryKey ? "primary key" : column.IsNullable ? "null" : "not null";

        return $"{columnName} {columnType} {columnNullability} {columnUnique}";
    }

    private static string GenerateColumnType(Type columnType)
    {
        if (columnType.FullName == typeof(string).FullName)
            return "text";

        if (columnType.FullName == typeof(int).FullName)
            return "integer";

        if (columnType.FullName == typeof(float).FullName ||
            columnType.FullName == typeof(double).FullName)
            return "real";

        if (columnType.FullName == typeof(DateTime).FullName)
            return "datetime";

        throw new NotSupportedException($"Column type not supported: {columnType}");
    }

    private static string PrintField(object? f)
    {
        if (f == null)
            return "null";

        if (f.GetType() == typeof(string))
            return $"\"{f}\"";

        return f.ToString()!;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = 200;
        context.HttpContext.Response.ContentType = "application/octet-stream";
        await context.HttpContext.Response.Body.WriteAsync(GetSQLiteDatabaseBuffer());
    }
}
