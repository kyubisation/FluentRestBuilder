namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using RestCollectionMutators.OrderBy;

    public interface IOrderByExpressionBuilder<TEntity>
    {
        IOrderByExpression<TEntity> Create(OrderByDirection direction);
    }
}
