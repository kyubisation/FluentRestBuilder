// <copyright file="IQueryablePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;

    public interface IQueryablePipeFactory<TInput, TOutput>
        where TInput : class, IQueryable
        where TOutput : class, IQueryable
    {
        OutputPipe<TOutput> Create(Func<TInput, TOutput> callback, IOutputPipe<TInput> parent);
    }
}
