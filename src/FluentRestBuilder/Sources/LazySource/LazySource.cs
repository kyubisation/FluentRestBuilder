// <copyright file="LazySource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class LazySource<TOutput> : SourceBase<TOutput>
    {
        private readonly Func<Task<TOutput>> output;

        public LazySource(
            Func<Task<TOutput>> output,
            IServiceProvider serviceProvider,
            ILogger<LazySource<TOutput>> logger = null)
            : base(logger, serviceProvider)
        {
            this.output = output;
        }

        public LazySource(
            Func<TOutput> output,
            IServiceProvider serviceProvider,
            ILogger<LazySource<TOutput>> logger = null)
            : this(() => Task.FromResult(output()), serviceProvider, logger)
        {
        }

        protected override Task<TOutput> GetOutput() => this.output();
    }
}
