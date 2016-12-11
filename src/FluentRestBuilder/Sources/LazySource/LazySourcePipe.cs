// <copyright file="LazySourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;
    using Common;

    public class LazySourcePipe<TOutput> : SourcePipe<TOutput>
    {
        private readonly Func<Task<TOutput>> output;

        public LazySourcePipe(Func<Task<TOutput>> output, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.output = output;
        }

        public LazySourcePipe(Func<TOutput> output, IServiceProvider serviceProvider)
            : this(() => Task.FromResult(output()), serviceProvider)
        {
        }

        protected override Task<TOutput> GetOutput() => this.output();
    }
}
