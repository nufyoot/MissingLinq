using MissingLinq.Attributes;

namespace MissingLinq.MySql.Benchmarks.TestTables;

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
