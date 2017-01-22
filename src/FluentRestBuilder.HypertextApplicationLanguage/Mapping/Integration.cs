// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Reflection;

    public static class Integration
    {
        internal static void RegisterMappingServices(this IServiceCollection services)
        {
            services.TryAddScoped<IMapperFactory, MapperFactory>();
            services.TryAddScoped(typeof(IMapperFactory<>), typeof(MapperFactory<>));
            services.TryAddTransient(typeof(IMappingBuilder<>), typeof(MappingBuilder<>));
            services.TryAddSingleton(typeof(IReflectionMapper<,>), typeof(ReflectionMapper<,>));
            services.TryAddScoped(typeof(IMapper<,>), typeof(ReflectionRestMapper<,>));
        }
    }
}
