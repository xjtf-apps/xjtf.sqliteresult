using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xjtf.AspNetCore.SQLiteResult;
namespace Xjtf.AspNetCore.SQLiteResult.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
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

        var sqliteResult = sqliteResultBuilder.Build();
    }
}