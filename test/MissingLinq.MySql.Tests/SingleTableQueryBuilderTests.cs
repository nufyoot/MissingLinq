namespace MissingLinq.MySql.Tests;

public class SingleTableQueryBuilderTests
{
    [Fact]
    public void TestBaseQuery()
    {
        var expected = "select age, another_column as AnotherColumn, name from people";
        var table = new SingleTableQueryBuilder<TestTables.Person>();
        var query = table.Build().ToString();

        Assert.Equal(expected, query);
    }

    [Fact]
    public void TestBaseQueryWithLimit()
    {
        var expected = "select age, another_column as AnotherColumn, name from people limit 10";
        var table = new SingleTableQueryBuilder<TestTables.Person>().Limit(10);
        var query = table.Build().ToString();

        Assert.Equal(expected, query);
    }
}



