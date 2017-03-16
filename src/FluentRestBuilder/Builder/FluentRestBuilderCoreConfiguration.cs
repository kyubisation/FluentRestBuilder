// <copyright file="FluentRestBuilderCoreConfiguration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public class FluentRestBuilderCoreConfiguration : IFluentRestBuilderCoreConfiguration
    {
        public FluentRestBuilderCoreConfiguration(IServiceCollection services)
        {
            this.Services = services;
            this.Services.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
        }

        public IServiceCollection Services { get; }
    }
}
