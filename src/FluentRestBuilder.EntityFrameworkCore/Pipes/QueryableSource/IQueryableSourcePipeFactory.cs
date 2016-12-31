// <copyright file="IQueryableSourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.QueryableSource
{
    using System;
    using System.Linq;
    using QueryableFactories;

    public interface IQueryableSourcePipeFactory<TInput, TOutput>
        where TOutput : class
    {
        QueryableSourcePipe<TInput, TOutput> Resolve(
            Func<IQueryableFactory, TInput, IQueryable<TOutput>> selection,
            IOutputPipe<TInput> pipe);
    }
}
