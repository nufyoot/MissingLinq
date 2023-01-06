using System.Collections.Concurrent;
using MissingLinq.Attributes;
using System.Reflection;

namespace MissingLinq.Resolvers;

/// <summary>
/// Represents the default implementation for resolving table names. This object only carries static state and therefore
/// should be reused.
/// </summary>
public class DefaultTableNameResolver : ITableNameResolver
{
    private static readonly ConcurrentDictionary<Type, string> ResolvedTableNames = new();
    
    /// <summary>
    /// Provides a common instance to be shared.
    /// </summary>
    public static DefaultTableNameResolver Instance { get; } = new DefaultTableNameResolver();

    /// <inheritdoc/>
    public virtual string Resolve(Type type)
    {
        if (!ResolvedTableNames.TryGetValue(type, out var resolvedName))
        {
            var tableNameAttribute = type.GetCustomAttribute<TableNameAttribute>();
            ResolvedTableNames.TryAdd(
                type,
                resolvedName = tableNameAttribute == null ? type.Name : tableNameAttribute.TableName);
        }

        return resolvedName;
    }
}
