// <copyright file="Source.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System;
    using System.Threading.Tasks;

    public class Source<TOutput> : BaseSource<TOutput>
    {
        private readonly Task<TOutput> output;

        public Source(Task<TOutput> output, IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.output = output;
        }

        public Source(TOutput output, IServiceProvider serviceProvider)
            : this(Task.FromResult(output), serviceProvider)
        {
        }

        protected override Task<TOutput> GetOutput() => this.output;
    }
}
