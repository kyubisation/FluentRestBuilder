// <copyright file="QueryablePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;

    public class QueryablePipeFactory<TInput> : IQueryablePipeFactory<TInput>
        where TInput : class
    {
        public OutputPipe<IQueryable<TInput>> Resolve(
            Func<IQueryable<TInput>, IQueryable<TInput>> callback,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new QueryablePipe<TInput>(callback, parent);
    }
}