using System.Collections.Concurrent;
using MissingLinq.Attributes;
using System.Reflection;

namespace MissingLinq.Resolvers;

/// <summary>
/// Represents the default implementation for resolving column names. This object only carries static state and therefore
/// should be reused.
/// </summary>
public class DefaultColumnNameResolver : IColumnNameResolver
{
    private static readonly ConcurrentDictionary<Type, (string PropertyName, string ColumnName)[]> ResolvedTableColumns = new();

    /// <summary>
    /// Provides a common instance to be shared.
    /// </summary>
    public static DefaultColumnNameResolver Instance { get; } = new();

    /// <inheritdoc/>
    public string Resolve(Type type, string propertyName)
    {
        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo == null)
        {
            throw new ArgumentException($"Unable to find a property named {propertyName} on object of type {type.FullName}", nameof(propertyName));
        }

        var columnNameAttribute = propertyInfo.GetCustomAttribute<ColumnNameAttribute>();
        return columnNameAttribute == null ? propertyInfo.Name : columnNameAttribute.ColumnName;
    }

    public (string PropertyName, string ColumnName)[] ResolveAllColumns(Type type)
    {
        if (!ResolvedTableColumns.TryGetValue(type, out var columns))
        {
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            columns = properties
                .Select(p => (PropertyName: p.Name, ColumnName: Resolve(type, p.Name)))
                .OrderBy(c => c.ColumnName)
                .ToArray();
            ResolvedTableColumns.TryAdd(type, columns);
        }

        return columns;
    }
}
