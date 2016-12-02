namespace KyubiCode.FluentRest.Common
{
    using System.Linq;

    public interface IQueryableFactory
    {
        IQueryable<TEntity> Resolve<TEntity>()
            where TEntity : class;
    }

    public interface IQueryableFactory<out TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Queryable { get; }
    }
}
