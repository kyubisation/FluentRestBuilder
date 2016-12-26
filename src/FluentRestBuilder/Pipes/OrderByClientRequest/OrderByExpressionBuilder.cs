namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System;
    using System.Linq.Expressions;
    using RestCollectionMutators.OrderBy;

    public class OrderByExpressionBuilder<TEntity, TKey> : IOrderByExpressionBuilder<TEntity>
    {
        private readonly Expression<Func<TEntity, TKey>> orderBy;

        public OrderByExpressionBuilder(Expression<Func<TEntity, TKey>> orderBy)
        {
            this.orderBy = orderBy;
        }

        public IOrderByExpression<TEntity> Create(OrderByDirection direction)
        {
            switch (direction)
            {
                case OrderByDirection.Descending:
                    return new DescendingOrderByExpression<TEntity, TKey>(this.orderBy);
                default:
                    return new AscendingOrderByExpression<TEntity, TKey>(this.orderBy);
            }
        }
    }
}