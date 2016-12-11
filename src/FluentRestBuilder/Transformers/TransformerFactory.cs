// <copyright file="TransformerFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Transformers
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class TransformerFactory : ITransformerFactory
    {
        private readonly IServiceProvider serviceProvider;

        public TransformerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITransformerFactory<TInput> Resolve<TInput>() =>
            this.serviceProvider.GetRequiredService<ITransformerFactory<TInput>>();
    }

#pragma warning disable SA1402 // File may only contain a single class
    public class TransformerFactory<TInput> : ITransformerFactory<TInput>
#pragma warning restore SA1402 // File may only contain a single class
    {
        private readonly IServiceProvider serviceProvider;

        public TransformerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ITransformer<TInput, TOutput> Resolve<TOutput>() =>
            this.serviceProvider.GetService<ITransformer<TInput, TOutput>>();
    }
}