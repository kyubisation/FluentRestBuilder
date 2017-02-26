// <copyright file="QueryablePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class QueryablePipe<TInput, TOutput> : MappingPipeBase<TInput, TOutput>
        where TInput : class, IQueryable
        where TOutput : class, IQueryable
    {
        private readonly Func<TInput, TOutput> callback;

        public QueryablePipe(
            Func<TInput, TOutput> callback,
            ILogger<QueryablePipe<TInput, TOutput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.callback = callback;
        }

        protected override TOutput Map(TInput input) => this.callback(input);
    }
}
