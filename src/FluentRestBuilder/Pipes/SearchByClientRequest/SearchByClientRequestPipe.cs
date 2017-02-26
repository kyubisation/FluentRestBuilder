// <copyright file="SearchByClientRequestPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Extensions.Logging;

    public class SearchByClientRequestPipe<TInput> : MappingPipeBase<IQueryable<TInput>, IQueryable<TInput>>
    {
        private readonly ISearchByClientRequestInterpreter interpreter;
        private readonly Func<string, Expression<Func<TInput, bool>>> search;

        public SearchByClientRequestPipe(
            ISearchByClientRequestInterpreter interpreter,
            Func<string, Expression<Func<TInput, bool>>> search,
            ILogger<SearchByClientRequestPipe<TInput>> logger,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(logger, parent)
        {
            this.interpreter = interpreter;
            this.search = search;
        }

        protected override IQueryable<TInput> Map(IQueryable<TInput> input)
        {
            var searchValue = this.interpreter.ParseRequestQuery();
            return searchValue == null ? input : this.ApplySearch(input, searchValue);
        }

        private IQueryable<TInput> ApplySearch(IQueryable<TInput> queryable, string searchValue) =>
            queryable.Where(this.search(searchValue));
    }
}
