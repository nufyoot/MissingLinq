using MissingLinq.Attributes;
using System.Reflection;

namespace MissingLinq.Resolvers;

/// <summary>
/// Represents the default implementation for resolving column names. This object only carries static state and therefore
/// should be reused.
/// </summary>
public class DefaultColumnNameResolver : IColumnNameResolver
{
    /// <summary>
    /// Provides a common instance to be shared.
    /// </summary>
    public static DefaultColumnNameResolver Instance { get; } = new DefaultColumnNameResolver();

    /// <inheritdoc/>
    public string Resolve(Type type, string propertyName)
    {
        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo == null)
        {
            throw new ArgumentException($"Unable to find a property named {propertyName} on object of type {type.FullName}", nameof(propertyName));
        }

        var columnNameAttribute = propertyInfo.GetCustomAttribute<ColumnNameAttribute>();
        if (columnNameAttribute == null)
        {
            return propertyInfo.Name;
        }

        return columnNameAttribute.ColumnName;
    }
}
