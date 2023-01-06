using MissingLinq.Attributes;
using System.Reflection;

namespace MissingLinq.Resolvers;

/// <summary>
/// Represents the default implementation for resolving table names. This object only carries static state and therefore
/// should be reused.
/// </summary>
public class DefaultTableNameResolver : ITableNameResolver
{
    /// <summary>
    /// Provides a common instance to be shared.
    /// </summary>
    public static DefaultTableNameResolver Instance { get; } = new DefaultTableNameResolver();

    /// <inheritdoc/>
    public string Resolve(Type type)
    {
        var tableNameAttribute = type.GetCustomAttribute<TableNameAttribute>();
        if (tableNameAttribute == null)
        {
            return type.Name;
        }

        return tableNameAttribute.TableName;
    }
}
