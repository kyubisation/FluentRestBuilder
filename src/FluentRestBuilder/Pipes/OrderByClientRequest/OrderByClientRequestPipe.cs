// <copyright file="OrderByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;
    using Microsoft.Extensions.Logging;

    public class OrderByClientRequestPipe<TInput> : MappingPipeBase<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary;
        private readonly IOrderByClientRequestInterpreter orderByClientRequestInterpreter;

        public OrderByClientRequestPipe(
            IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary,
            IOrderByClientRequestInterpreter orderByClientRequestInterpreter,
            ILogger<OrderByClientRequestPipe<TInput>> logger,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(logger, parent)
        {
            this.orderByDictionary = orderByDictionary;
            this.orderByClientRequestInterpreter = orderByClientRequestInterpreter;
        }

        protected override IQueryable<TInput> Map(IQueryable<TInput> input)
        {
            var orderByExpressions = this.ResolveOrderBySequence();
            return orderByExpressions.Count == 0
                ? input : this.ApplyOrderBy(orderByExpressions, input);
        }

        private List<IOrderByExpression<TInput>> ResolveOrderBySequence() =>
            this.orderByClientRequestInterpreter
                .ParseRequestQuery(this.orderByDictionary.Keys)
                .Select(this.CreateExpression)
                .ToList();

        private IOrderByExpression<TInput> CreateExpression(OrderByRequest request)
        {
            this.Logger.Debug?.Log("Resolving order by expression for {0}", request);
            return this.orderByDictionary[request.Property].Create(request.Direction);
        }

        private IQueryable<TInput> ApplyOrderBy(
            List<IOrderByExpression<TInput>> orderByExpressions, IQueryable<TInput> queryable)
        {
            var firstOrderByValue = orderByExpressions.First();
            return orderByExpressions
                .Skip(1)
                .Aggregate(
                    firstOrderByValue.OrderBy(queryable),
                    (current, orderBy) => orderBy.ThenBy(current));
        }
    }
}
