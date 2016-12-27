// <copyright file="SourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System;
    using System.Threading.Tasks;

    public class SourceFactory<TOutput> : ISourceFactory<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        public SourceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TOutput> Resolve(Task<TOutput> output) =>
            new Source<TOutput>(output, this.serviceProvider);

        public OutputPipe<TOutput> Resolve(TOutput output) =>
            new Source<TOutput>(output, this.serviceProvider);
    }
}