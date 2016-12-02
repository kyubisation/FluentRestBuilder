namespace KyubiCode.FluentRest.RestCollectionMutators.Common
{
    using System.Linq;

    public interface IRestCollectionMutator<TEntity>
    {
        IQueryable<TEntity> Apply(IQueryable<TEntity> queryable);
    }
}