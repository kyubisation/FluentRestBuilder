// <copyright file="ILazySourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;

    public interface ILazySourcePipeFactory<TOutput>
    {
        OutputPipe<TOutput> Resolve(Func<Task<TOutput>> output);

        OutputPipe<TOutput> Resolve(Func<TOutput> output);
    }
}
