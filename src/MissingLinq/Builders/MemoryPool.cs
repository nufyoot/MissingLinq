using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.InteropServices;

namespace MissingLinq.Builders;

internal sealed class MemoryPool<T>
{
    private static readonly ConcurrentDictionary<uint, ConcurrentQueue<Memory<T>>> Pool = new();

    public Memory<T> Rent(uint size)
    {
        var snapSize = RoundUpToNearestPowerOfTwo(size);
        if (!snapSize.HasValue)
        {
            throw new ArgumentOutOfRangeException(
                nameof(size),
                "Requested span size is too large of this implementation.");
        }

        var poolQueue = Pool.GetOrAdd(snapSize.GetValueOrDefault(), _ => new());
        if (!poolQueue.TryDequeue(out var result))
        {
            result = new T[snapSize.GetValueOrDefault()].AsMemory();
        }

        return result;
    }

    public void Return(Memory<T> rented)
    {
        var poolQueue = Pool.GetOrAdd((uint)rented.Length, _ => new());
        poolQueue.Enqueue(rented);
    }

    private static uint? RoundUpToNearestPowerOfTwo(uint value)
    {
        const uint maxSize = (uint)1 << 31;
        if (value > maxSize)
        {
            return null;
        }

        var numberOfBitsNeeded = (sizeof(uint) * 8) - BitOperations.LeadingZeroCount(value - 1);
        return (uint)1 << numberOfBitsNeeded;
    }
}