// <copyright file="QueryablePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;

    public class QueryablePipeFactory<TInput, TOutput> : IQueryablePipeFactory<TInput, TOutput>
        where TInput : class, IQueryable
        where TOutput : class, IQueryable
    {
        public OutputPipe<TOutput> Create(
            Func<TInput, TOutput> callback, IOutputPipe<TInput> parent) =>
            new QueryablePipe<TInput, TOutput>(callback, parent);
    }
}