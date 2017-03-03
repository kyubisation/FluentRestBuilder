// <copyright file="Source.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class Source<TOutput> : SourceBase<TOutput>
    {
        private readonly Task<TOutput> output;

        public Source(
            Task<TOutput> output,
            IServiceProvider serviceProvider,
            ILogger<Source<TOutput>> logger = null)
            : base(logger, serviceProvider)
        {
            this.output = output;
        }

        public Source(
            TOutput output,
            IServiceProvider serviceProvider,
            ILogger<Source<TOutput>> logger = null)
            : this(Task.FromResult(output), serviceProvider, logger)
        {
        }

        protected override Task<TOutput> GetOutput() => this.output;
    }
}
