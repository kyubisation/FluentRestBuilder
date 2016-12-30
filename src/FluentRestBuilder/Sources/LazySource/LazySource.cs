// <copyright file="LazySource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;

    public class LazySource<TOutput> : SourceBase<TOutput>
    {
        private readonly Func<Task<TOutput>> output;

        public LazySource(Func<Task<TOutput>> output, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.output = output;
        }

        public LazySource(Func<TOutput> output, IServiceProvider serviceProvider)
            : this(() => Task.FromResult(output()), serviceProvider)
        {
        }

        protected override Task<TOutput> GetOutput() => this.output();
    }
}
