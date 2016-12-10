// <copyright file="ILazySourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;

    public interface ILazySourcePipeFactory<TOutput>
    {
        LazySourcePipe<TOutput> Resolve(Func<Task<TOutput>> output);

        LazySourcePipe<TOutput> Resolve(Func<TOutput> output);
    }
}
