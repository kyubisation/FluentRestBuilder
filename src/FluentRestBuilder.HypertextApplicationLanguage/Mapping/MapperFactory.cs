// <copyright file="MapperFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class MapperFactory : IMapperFactory
    {
        private readonly IServiceProvider serviceProvider;

        public MapperFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IMapperFactory<TInput> Resolve<TInput>() =>
            this.serviceProvider.GetRequiredService<IMapperFactory<TInput>>();
    }

#pragma warning disable SA1402 // File may only contain a single class
    public class MapperFactory<TInput> : IMapperFactory<TInput>
#pragma warning restore SA1402 // File may only contain a single class
    {
        private readonly IServiceProvider serviceProvider;

        public MapperFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IMapper<TInput, TOutput> Resolve<TOutput>() =>
            this.serviceProvider.GetService<IMapper<TInput, TOutput>>();
    }
}