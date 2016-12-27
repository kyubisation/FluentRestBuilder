// <copyright file="OrderByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;

    public class OrderByClientRequestPipe<TInput> : BaseMappingPipe<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary;
        private readonly IOrderByClientRequestInterpreter orderByClientRequestInterpreter;

        public OrderByClientRequestPipe(
            IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary,
            IOrderByClientRequestInterpreter orderByClientRequestInterpreter,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
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
                .ParseRequestQuery()
                .Where(o => this.orderByDictionary.ContainsKey(o.Item1))
                .Select(o => this.orderByDictionary[o.Item1].Create(o.Item2))
                .ToList();

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
