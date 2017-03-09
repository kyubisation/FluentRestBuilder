// <copyright file="FilterByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Expressions;
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

        protected override IQueryable<TInput> Map(IQueryable<TInput> input)
        {
            var filterRequests = this.interpreter.ParseRequestQuery(this.filterDictionary.Keys);
            return this.ApplyFilters(filterRequests, input);
        }

        private IQueryable<TInput> ApplyFilters(
            IEnumerable<FilterRequest> filterRequests, IQueryable<TInput> queryable) =>
            filterRequests
                .Select(this.ResolveFilterExpression)
                .Where(f => f != null)
                .Aggregate(queryable, (current, next) => current.Where(next));

        private Expression<Func<TInput, bool>> ResolveFilterExpression(FilterRequest request)
        {
            this.Logger.Debug?.Log("Attempting to filter according to {0}", request);
            return this.filterDictionary[request.Property].Resolve(request.Type, request.Filter);
        }
    }
}
