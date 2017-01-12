// <copyright file="LazySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class LazySourceFactory<TInput> : ILazySourceFactory<TInput>
    {
        private readonly IServiceProvider serviceProvider;

        public LazySourceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TInput> Create(Func<Task<TInput>> output, ControllerBase controller) =>
            new LazySource<TInput>(output, this.serviceProvider) { Controller = controller };
    }
}