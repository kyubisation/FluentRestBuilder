// <copyright file="LazySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;

    public class LazySourceFactory<TOutput> : ILazySourceFactory<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        public LazySourceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TOutput> Resolve(Func<Task<TOutput>> output) =>
            new LazySource<TOutput>(output, this.serviceProvider);

        public OutputPipe<TOutput> Resolve(Func<TOutput> output) =>
            new LazySource<TOutput>(output, this.serviceProvider);
    }
}