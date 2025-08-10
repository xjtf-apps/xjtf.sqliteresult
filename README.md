# SQLiteResult

Simple SQLite schema builder and data loader for AspNetCore actions. Returns a client ready SQLite database for local-first scenarios.

## Usage

```csharp
[HttpGet]
public async Task<IActionResult> GetLocalData()
{
	var sqliteResultBuilder = new SQLiteResultBuilder();
        sqliteResultBuilder
        .Table("users")
        .Column(new SQLiteColumnBuilder()
        {
            ColumnName = "id",
            ColumnType = typeof(int),
            IsAutoIncrement = true,
            IsPrimaryKey = true
        })
        .Column(new SQLiteColumnBuilder()
        {
            ColumnName = "email",
            ColumnType = typeof(string),
        })
        .Column(new SQLiteColumnBuilder()
        {
            ColumnName = "firstName",
            ColumnType = typeof(string),
        })
        .Column(new SQLiteColumnBuilder()
        {
            ColumnName = "lastName",
            ColumnType = typeof(string),
        });

    sqliteResultBuilder["users"].Rows.AddRange([
        new SQLiteRowData([1, "test@test.com", "mark", "anthony"])
    ]);

    return sqliteResultBuilder.Build();
}
```