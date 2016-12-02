namespace KyubiCode.FluentRest.SourcePipes.EntityCollection
{
    using System;
    using FluentRest.Common;

    public class EntityCollectionSourceFactory<TEntity> : IEntityCollectionSourceFactory<TEntity>
        where TEntity : class
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IQueryableFactory<TEntity> queryableFactory;

        public EntityCollectionSourceFactory(
            IServiceProvider serviceProvider,
            IQueryableFactory<TEntity> queryableFactory)
        {
            this.serviceProvider = serviceProvider;
            this.queryableFactory = queryableFactory;
        }

        public EntityCollectionSource<TEntity> Resolve() =>
            new EntityCollectionSource<TEntity>(
                this.queryableFactory.Queryable, this.serviceProvider);
    }
}
