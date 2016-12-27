// <copyright file="LazySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.LazySource
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class LazySourceFactory<TOutput> : ILazySourceFactory<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        public LazySourceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TOutput> Resolve(Func<Task<TOutput>> output, IUrlHelper urlHelper) =>
            new LazySource<TOutput>(output, this.serviceProvider) { UrlHelper = urlHelper };
    }
}