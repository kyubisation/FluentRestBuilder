// <copyright file="SearchByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Extensions.Logging;

    public class SearchByClientRequestPipeFactory<TInput> : ISearchByClientRequestPipeFactory<TInput>
    {
        private readonly ISearchByClientRequestInterpreter interpreter;
        private readonly ILogger<SearchByClientRequestPipe<TInput>> logger;

        public SearchByClientRequestPipeFactory(
            ISearchByClientRequestInterpreter interpreter,
            ILogger<SearchByClientRequestPipe<TInput>> logger = null)
        {
            this.interpreter = interpreter;
            this.logger = logger;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            Func<string, Expression<Func<TInput, bool>>> search,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new SearchByClientRequestPipe<TInput>(this.interpreter, search, this.logger, parent);
    }
}