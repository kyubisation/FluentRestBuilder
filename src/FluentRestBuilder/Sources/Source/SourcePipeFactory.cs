// <copyright file="SourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sources.Source
{
    using System;
    using System.Threading.Tasks;

    public class SourcePipeFactory<TOutput> : ISourcePipeFactory<TOutput>
    {
        private readonly IServiceProvider serviceProvider;

        public SourcePipeFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TOutput> Resolve(Task<TOutput> output) =>
            new SourcePipe<TOutput>(output, this.serviceProvider);

        public OutputPipe<TOutput> Resolve(TOutput output) =>
            new SourcePipe<TOutput>(output, this.serviceProvider);
    }
}