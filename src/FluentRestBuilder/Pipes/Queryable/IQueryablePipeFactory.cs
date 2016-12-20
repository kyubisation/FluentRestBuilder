// <copyright file="IQueryablePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;

    public interface IQueryablePipeFactory<TInput>
    {
        OutputPipe<IQueryable<TInput>> Resolve(
            Func<IQueryable<TInput>, IQueryable<TInput>> callback,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
