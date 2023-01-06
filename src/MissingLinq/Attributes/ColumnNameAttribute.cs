namespace MissingLinq.Attributes;

/// <summary>
/// Represents the name of a column, regardless of the underlying data source.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ColumnNameAttribute : Attribute
{
    /// <summary>
    /// Constructs a new attribute that represents the name of a column.
    /// </summary>
    /// <param name="columnName">The name of the column.</param>
    public ColumnNameAttribute(string columnName)
    {
        ColumnName = columnName;
    }

    /// <summary>
    /// Gets the name of the column.
    /// </summary>
    public virtual string ColumnName { get; }
}
