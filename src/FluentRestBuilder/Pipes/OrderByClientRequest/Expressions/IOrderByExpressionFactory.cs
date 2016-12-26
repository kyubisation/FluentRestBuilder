namespace FluentRestBuilder.Pipes.OrderByClientRequest.Expressions
{
    using RestCollectionMutators.OrderBy;

    public interface IOrderByExpressionFactory<TEntity>
    {
        IOrderByExpression<TEntity> Create(OrderByDirection direction);
    }
}
