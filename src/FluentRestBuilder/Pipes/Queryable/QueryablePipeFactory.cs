// <copyright file="QueryablePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class QueryablePipeFactory<TInput, TOutput> : IQueryablePipeFactory<TInput, TOutput>
        where TInput : class, IQueryable
        where TOutput : class, IQueryable
    {
        private readonly ILogger<QueryablePipe<TInput, TOutput>> logger;

        public QueryablePipeFactory(ILogger<QueryablePipe<TInput, TOutput>> logger = null)
        {
            this.logger = logger;
        }

        public OutputPipe<TOutput> Create(
            Func<TInput, TOutput> callback, IOutputPipe<TInput> parent) =>
            new QueryablePipe<TInput, TOutput>(callback, this.logger, parent);
    }
}