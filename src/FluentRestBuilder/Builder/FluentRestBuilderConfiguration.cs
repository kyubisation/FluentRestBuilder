// <copyright file="FluentRestBuilderConfiguration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using Microsoft.Extensions.DependencyInjection;

    public class FluentRestBuilderConfiguration : IFluentRestBuilderConfiguration
    {
        public FluentRestBuilderConfiguration(IServiceCollection services)
        {
            this.Services = services;
            new FluentRestBuilderCoreConfiguration(this.Services)
                .RegisterSource()
                .RegisterLazySource()
                .RegisterFilterByClientRequestPipe()
                .RegisterMappingPipe()
                .RegisterOrderByClientRequestPipe()
                .RegisterPaginationByClientRequestPipe()
                .RegisterQueryablePipe();
        }

        public IServiceCollection Services { get; }
    }
}