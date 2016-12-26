namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Linq;

    public interface IOrderByExpression<TEntity>
    {
        IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> queryable);

        IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> queryable);
    }
}
