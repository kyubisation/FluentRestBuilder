// <copyright file="LazySourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;

    public class LazySourcePipeFactory<TOutput> : ILazySourcePipeFactory<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        public LazySourcePipeFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public LazySourcePipe<TOutput> Resolve(Func<Task<TOutput>> output) =>
            new LazySourcePipe<TOutput>(output, this.serviceProvider);

        public LazySourcePipe<TOutput> Resolve(Func<TOutput> output) =>
            new LazySourcePipe<TOutput>(output, this.serviceProvider);
    }
}