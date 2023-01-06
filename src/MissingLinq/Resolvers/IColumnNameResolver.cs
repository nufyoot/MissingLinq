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

    (string PropertyName, string ColumnName)[] ResolveAllColumns<T>() { return ResolveAllColumns(typeof(T)); }

    (string PropertyName, string ColumnName)[] ResolveAllColumns(Type type);
}
