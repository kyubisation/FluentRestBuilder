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
    using Exceptions;
    using Expressions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class FilterByClientRequestPipe<TInput> : MappingPipeBase<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly IDictionary<string, IFilterExpressionProvider<TInput>> filterDictionary;
        private readonly IFilterByClientRequestInterpreter interpreter;

        public FilterByClientRequestPipe(
            IDictionary<string, IFilterExpressionProvider<TInput>> filterDictionary,
            IFilterByClientRequestInterpreter interpreter,
            ILogger<FilterByClientRequestPipe<TInput>> logger,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(logger, parent)
        {
            this.filterDictionary = filterDictionary;
            this.interpreter = interpreter;
        }

        protected override async Task<IActionResult> Execute(IQueryable<TInput> input)
        {
            try
            {
                return await base.Execute(input);
            }
            catch (FilterException exception)
            {
                this.Logger.Information?.Log(0, exception, "Filtering failed");
                return new BadRequestObjectResult(new { error = exception.Message });
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
            this.Logger.Debug?.Log("Attempting to filter according to {0}", request);
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
