namespace MissingLinq.MySql.Tests.TestTables;

[TableName("people")]
public class Person
{
    [ColumnName("name")]
    public string? Name { get; set; }

    [ColumnName("age")]
    public int Age { get; set; }

    [ColumnName("another_column")]
    public int AnotherColumn { get; set; }
}
