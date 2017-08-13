// <copyright file="SourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class SourceFactory<TOutput> : ISourceFactory<TOutput>
    {
        private readonly ILogger<Source<TOutput>> logger;
        private readonly IServiceProvider serviceProvider;

        public SourceFactory(
            IServiceProvider serviceProvider,
            ILogger<Source<TOutput>> logger = null)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TOutput> Create(Task<TOutput> output, ControllerBase controller) =>
            new Source<TOutput>(output, this.serviceProvider, this.logger);
    }
}