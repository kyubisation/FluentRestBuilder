namespace KyubiCode.FluentRest.SourcePipes.SelectableEntity
{
    using System;
    using System.Linq.Expressions;

    public interface ISelectableEntitySourceFactory<TEntity>
        where TEntity : class
    {
        SelectableEntitySource<TEntity> Resolve(Expression<Func<TEntity, bool>> selectionFilter);
    }
}