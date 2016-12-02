namespace KyubiCode.FluentRest.Common
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class QueryableFactory : IQueryableFactory
    {
        private readonly DbContext context;

        public QueryableFactory(DbContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Resolve<TEntity>()
            where TEntity : class =>
            this.context.Set<TEntity>();
    }

    public class QueryableFactory<TEntity> : IQueryableFactory<TEntity>
        where TEntity : class
    {
        public QueryableFactory(IQueryableFactory queryableFactory)
        {
            this.Queryable = queryableFactory.Resolve<TEntity>();
        }

        public IQueryable<TEntity> Queryable { get; }
    }
}