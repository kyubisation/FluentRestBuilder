// <copyright file="FilterByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;

    public class FilterByClientRequestPipeFactory<TInput> : IFilterByClientRequestPipeFactory<TInput>
    {
        private readonly IFilterByClientRequestInterpreter interpreter;

        public FilterByClientRequestPipeFactory(IFilterByClientRequestInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            IDictionary<string, IFilterExpressionProvider<TInput>> filterDictionary,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new FilterByClientRequestPipe<TInput>(filterDictionary, this.interpreter, parent);
    }
}