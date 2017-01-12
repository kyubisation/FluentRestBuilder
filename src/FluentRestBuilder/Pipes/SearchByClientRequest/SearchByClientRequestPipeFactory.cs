// <copyright file="SearchByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class SearchByClientRequestPipeFactory<TInput> : ISearchByClientRequestPipeFactory<TInput>
    {
        private readonly ISearchByClientRequestInterpreter interpreter;

        public SearchByClientRequestPipeFactory(ISearchByClientRequestInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            Func<string, Expression<Func<TInput, bool>>> search,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new SearchByClientRequestPipe<TInput>(this.interpreter, search, parent);
    }
}