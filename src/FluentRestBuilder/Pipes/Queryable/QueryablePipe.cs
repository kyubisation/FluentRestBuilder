// <copyright file="QueryablePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;

    public class QueryablePipe<TInput, TOutput> : MappingPipeBase<TInput, TOutput>
        where TInput : class, IQueryable
        where TOutput : class, IQueryable
    {
        private readonly Func<TInput, TOutput> callback;

        public QueryablePipe(
            Func<TInput, TOutput> callback,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.callback = callback;
        }

        protected override TOutput Map(TInput input) => this.callback(input);
    }
}
