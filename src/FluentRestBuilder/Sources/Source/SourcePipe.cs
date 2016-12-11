// <copyright file="SourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System;
    using System.Threading.Tasks;

    public class SourcePipe<TOutput> : Common.SourcePipe<TOutput>
    {
        private readonly Task<TOutput> output;

        public SourcePipe(Task<TOutput> output, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.output = output;
        }

        public SourcePipe(TOutput output, IServiceProvider serviceProvider)
            : this(Task.FromResult(output), serviceProvider)
        {
        }

        protected override Task<TOutput> GetOutput() => this.output;
    }
}
