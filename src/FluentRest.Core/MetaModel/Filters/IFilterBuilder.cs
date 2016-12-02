namespace KyubiCode.FluentRest.MetaModel.Filters
{
    using System;
    using System.Linq.Expressions;

    public interface IFilterBuilder<TEntity>
    {
        Expression<Func<TEntity, bool>> CreateFilter(object filter);
    }
}