// <copyright file="SourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class SourceFactory<TOutput> : ISourceFactory<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        public SourceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TOutput> Resolve(Task<TOutput> output, IUrlHelper urlHelper) =>
            new Source<TOutput>(output, this.serviceProvider) { UrlHelper = urlHelper };
    }
}