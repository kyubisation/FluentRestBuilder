// <copyright file="OrderByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Exceptions;
    using Expressions;
    using Microsoft.AspNetCore.Mvc;
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

        protected override async Task<IActionResult> Execute(IQueryable<TInput> input)
        {
            try
            {
                return await base.Execute(input);
            }
            catch (OrderByNotSupportedException exception)
            {
                this.Logger.Information?.Log(0, exception, "Ordering failed");
                return new BadRequestObjectResult(new { error = exception.Message });
            }
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
                .Select(this.CreateExpression)
                .ToList();

        private IOrderByExpression<TInput> CreateExpression(OrderByRequest request)
        {
            this.Logger.Debug?.Log("Attempting to order according to {0}", request);
            return this.ResolveFactory(request).Create(request.Direction);
        }

        private IOrderByExpressionFactory<TInput> ResolveFactory(OrderByRequest request)
        {
            IOrderByExpressionFactory<TInput> factory;
            if (!this.orderByDictionary.TryGetValue(request.Property, out factory))
            {
                throw new OrderByNotSupportedException(request.OriginalProperty);
            }

            return factory;
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
