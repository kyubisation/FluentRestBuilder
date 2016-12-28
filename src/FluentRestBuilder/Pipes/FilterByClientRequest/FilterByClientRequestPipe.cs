// <copyright file="FilterByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Expressions;
    using Microsoft.AspNetCore.Mvc;

    public class FilterByClientRequestPipe<TInput> : BaseMappingPipe<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly IDictionary<string, IFilterExpressionProvider<TInput>> filterDictionary;
        private readonly IFilterByClientRequestInterpreter interpreter;

        public FilterByClientRequestPipe(
            IDictionary<string, IFilterExpressionProvider<TInput>> filterDictionary,
            IFilterByClientRequestInterpreter interpreter,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.filterDictionary = filterDictionary;
            this.interpreter = interpreter;
        }

        protected override Task<IActionResult> Execute(IQueryable<TInput> input)
        {
            try
            {
                return base.Execute(input);
            }
            catch (FilterException exception)
            {
                return Task.FromResult<IActionResult>(
                    new BadRequestObjectResult(new { error = exception.Message }));
            }
        }

        protected override IQueryable<TInput> Map(IQueryable<TInput> input)
        {
            var filterRequests = this.interpreter.ParseRequestQuery();
            return this.ApplyFilters(filterRequests, input);
        }

        private IQueryable<TInput> ApplyFilters(
            IEnumerable<FilterRequest> filterRequests, IQueryable<TInput> queryable) =>
            filterRequests
                .Select(this.ResolveFilterExpression)
                .Aggregate(queryable, (current, next) => current.Where(next));

        private Expression<Func<TInput, bool>> ResolveFilterExpression(FilterRequest request)
        {
            IFilterExpressionProvider<TInput> provider;
            this.filterDictionary.TryGetValue(request.Property, out provider);
            var expression = provider?.Resolve(request.Type, request.Filter);
            if (expression == null)
            {
                throw new FilterNotSupportedException(request.OriginalProperty);
            }

            return expression;
        }
    }
}
