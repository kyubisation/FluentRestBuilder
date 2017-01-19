// <copyright file="FluentRestBuilderCore.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Storage;

    public class FluentRestBuilderCore : IFluentRestBuilderCore
    {
        public FluentRestBuilderCore(IServiceCollection services)
        {
            this.Services = services;
            this.Services.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
        }

        public IServiceCollection Services { get; }
    }
}
