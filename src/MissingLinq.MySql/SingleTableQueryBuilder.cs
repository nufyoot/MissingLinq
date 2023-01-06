using MissingLinq.Resolvers;

namespace MissingLinq.MySql;

/// <summary>
/// Provides common functions for building out a query against MySQL.
/// </summary>
/// <typeparam name="T">The MySQL table object.</typeparam>
public class SingleTableQueryBuilder<T>
{
    private readonly ITableNameResolver _tableNameResolver;
    private readonly IColumnNameResolver _columnNameResolver;

    public SingleTableQueryBuilder()
        : this(DefaultTableNameResolver.Instance, DefaultColumnNameResolver.Instance)
    {
    }

    public SingleTableQueryBuilder(ITableNameResolver tableNameResolver, IColumnNameResolver columnNameResolver)
    {
        _tableNameResolver = tableNameResolver;
        _columnNameResolver = columnNameResolver;
    }

    /// <summary>
    /// Gets or sets the limit for the number of results to pull.
    /// </summary>
    public virtual int? ResultLimit { get; set; }

    public virtual SingleTableQueryBuilder<T> Limit(int? limit)
    {
        ResultLimit = limit;
        return this;
    }

    public virtual StringBuilder Build()
    {
        var builder = new StringBuilder();
        Build(builder);
        return builder;
    }

    public virtual void Build(StringBuilder builder)
    {
        BuildSelect(builder);
        builder.Append(' ');
        BuildFrom(builder);
        if (ResultLimit.HasValue)
        {
            builder.Append(' ');
            BuildLimit(builder);
        }
    }

    public virtual void BuildSelect(StringBuilder builder)
    {
        var columns = _columnNameResolver.ResolveAllColumns<T>();
        builder.Append("select ");

        foreach (var column in columns)
        {
            if (column.ColumnName.Equals(column.PropertyName, StringComparison.OrdinalIgnoreCase))
            {
                builder.Append(column.ColumnName);
            }
            else
            {
                builder.Append($"{column.ColumnName} as {column.PropertyName}");
            }

            builder.Append(", ");
        }

        if (columns.Length > 0)
        {
            builder.Length -= 2;
        }
    }

    public virtual void BuildFrom(StringBuilder builder)
    {
        builder.Append("from ");
        builder.Append(_tableNameResolver.Resolve<T>());
    }

    public virtual void BuildLimit(StringBuilder buidler)
    {
        if (ResultLimit.HasValue)
        {
            buidler.Append($"limit {ResultLimit.GetValueOrDefault()}");
        }
    }
}
