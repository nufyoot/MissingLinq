using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MissingLinq.MySql;
using MissingLinq.MySql.Benchmarks.TestTables;

BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser]
public class Benchmarks
{
    [Benchmark]
    public void TestBuildingQuery()
    {
        var table = new SingleTableQueryBuilder<Person>();
        table.Build().ToString();
    }
}