// <copyright file="FilterByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;
    using Microsoft.Extensions.Logging;

    public class FilterByClientRequestPipeFactory<TInput> : IFilterByClientRequestPipeFactory<TInput>
    {
        private readonly IFilterByClientRequestInterpreter interpreter;
        private readonly ILogger<FilterByClientRequestPipe<TInput>> logger;

        public FilterByClientRequestPipeFactory(
            IFilterByClientRequestInterpreter interpreter,
            ILogger<FilterByClientRequestPipe<TInput>> logger = null)
        {
            this.interpreter = interpreter;
            this.logger = logger;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            IDictionary<string, IFilterExpressionProvider<TInput>> filterDictionary,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new FilterByClientRequestPipe<TInput>(
                filterDictionary, this.interpreter, this.logger, parent);
    }
}