// <copyright file="LazySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class LazySourceFactory<TInput> : ILazySourceFactory<TInput>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<LazySource<TInput>> logger;

        public LazySourceFactory(
            IServiceProvider serviceProvider,
            ILogger<LazySource<TInput>> logger = null)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(Func<Task<TInput>> output, ControllerBase controller) =>
            new LazySource<TInput>(output, this.serviceProvider, this.logger)
            {
                Controller = controller,
            };
    }
}