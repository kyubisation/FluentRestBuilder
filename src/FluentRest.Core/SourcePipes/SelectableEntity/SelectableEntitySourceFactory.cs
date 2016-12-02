namespace KyubiCode.FluentRest.SourcePipes.SelectableEntity
{
    using System;
    using System.Linq.Expressions;
    using FluentRest.Common;

    public class SelectableEntitySourceFactory<TEntity> : ISelectableEntitySourceFactory<TEntity>
        where TEntity : class
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IQueryableFactory<TEntity> queryableFactory;

        public SelectableEntitySourceFactory(
            IServiceProvider serviceProvider,
            IQueryableFactory<TEntity> queryableFactory)
        {
            this.serviceProvider = serviceProvider;
            this.queryableFactory = queryableFactory;
        }

        public SelectableEntitySource<TEntity> Resolve(
            Expression<Func<TEntity, bool>> selectionFilter) =>
            new SelectableEntitySource<TEntity>(
                selectionFilter, this.queryableFactory.Queryable, this.serviceProvider);
    }
}
