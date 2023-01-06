namespace MissingLinq.Resolvers;

/// <summary>
/// Represents an object that is able to resolve the name of a table.
/// </summary>
public interface ITableNameResolver
{
    /// <summary>
    /// Resolves a given type into a table name.
    /// </summary>
    /// <typeparam name="T">The object type to be resolved into a name.</typeparam>
    /// <returns>Returns the string name for a given object type.</returns>
    string Resolve<T>() { return Resolve(typeof(T)); }

    /// <summary>
    /// Resolves a given type into a table name.
    /// </summary>
    /// <param name="type">The object type to be resolved into a name.</param>
    /// <returns>Returns the string name for a given object type.</returns>
    string Resolve(Type type);
}
