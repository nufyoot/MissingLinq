using System.Linq.Expressions;
using System.Reflection;

namespace MissingLinq.Resolvers;

/// <summary>
/// Represents an object that is able to resolve the name of a column.
/// </summary>
public interface IColumnNameResolver
{
    string Resolve<T>(string propertyName) { return Resolve(typeof(T), propertyName); }

    string Resolve(Type type, string propertyName);

    IEnumerable<(string PropertyName, string ColumnName)> ResolveAllColumns<T>() { return ResolveAllColumns(typeof(T)); }

    IEnumerable<(string PropertyName, string ColumnName)> ResolveAllColumns(Type type)
    {
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        return properties
            .Select(p => (PropertyName: p.Name, ColumnName: Resolve(type, p.Name)))
            .OrderBy(c => c.ColumnName);
    }
}
