using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MissingLinq.Builders;

BenchmarkRunner.Run<Benchmarks>();

[MemoryDiagnoser]
public class Benchmarks
{
    private readonly MemoryPool<char> pool = new();

    [Benchmark]
    public void TestRentingSpan()
    {
        var rentedChunk = pool.Rent(99);
        pool.Return(rentedChunk);
    }
}