namespace MissingLinq.Attributes;

/// <summary>
/// Represents the name of a table, regardless of the underlying data source.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TableNameAttribute : Attribute
{
    /// <summary>
    /// Constructs a new attribute that represents the name of a table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    public TableNameAttribute(string tableName)
    {
        TableName = tableName;
    }

    /// <summary>
    /// Gets the name of the table.
    /// </summary>
    public virtual string TableName { get; }
}
