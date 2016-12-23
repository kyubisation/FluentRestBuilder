// <copyright file="FluentRestBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using System.Linq;
    using Common;
    using Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.Actions;
    using Pipes.ClaimValidation;
    using Pipes.CollectionMapping;
    using Pipes.EntityValidation;
    using Pipes.Mapping;
    using Pipes.Queryable;
    using Pipes.Validation;
    using Results.Options;
    using Sources.LazySource;
    using Sources.Source;
    using Storage;

    public class FluentRestBuilder : IFluentRestBuilder
    {
        public FluentRestBuilder(IServiceCollection services)
        {
            this.Services = services;
            RegisterPipeFactories(this.Services);
            RegisterMappings(this.Services);
            RegisterUtilities(this.Services);
        }

        public IServiceCollection Services { get; }

        private static void RegisterPipeFactories(IServiceCollection collection)
        {
            collection.TryAddScoped(
                typeof(ISourcePipeFactory<>), typeof(SourcePipeFactory<>));
            collection.TryAddScoped(
                typeof(ILazySourcePipeFactory<>), typeof(LazySourcePipeFactory<>));
            collection.TryAddScoped(
                typeof(IActionPipeFactory<>), typeof(ActionPipeFactory<>));
            collection.TryAddScoped(
                typeof(IClaimValidationPipeFactory<>), typeof(ClaimValidationPipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityValidationPipeFactory<>), typeof(EntityValidationPipeFactory<>));
            collection.TryAddScoped(
                typeof(IMappingPipeFactory<,>), typeof(MappingPipeFactory<,>));
            collection.TryAddScoped(
                typeof(ICollectionMappingPipeFactory<,>), typeof(CollectionMappingPipeFactory<,>));
            collection.TryAddScoped(
                typeof(IValidationPipeFactory<>), typeof(ValidationPipeFactory<>));
            collection.TryAddScoped(
                typeof(IQueryablePipeFactory<,>), typeof(QueryablePipeFactory<,>));
        }

        private static void RegisterMappings(IServiceCollection collection)
        {
            collection.TryAddScoped<IMapperFactory, MapperFactory>();
            collection.TryAddScoped(typeof(IMapperFactory<>), typeof(MapperFactory<>));
            collection.TryAddTransient(
                typeof(IMappingBuilder<>), typeof(MappingBuilder<>));
        }

        private static void RegisterUtilities(IServiceCollection collection)
        {
            collection.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            collection.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            collection.TryAddTransient(typeof(LazyResolver<>));
            collection.TryAddScoped(typeof(IAllowedOptionsBuilder<>), typeof(AllowedOptionsBuilder<>));
            collection.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
            collection.TryAddScoped(
                s => s.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Query);

            collection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            if (collection.All(d => d.ServiceType != typeof(IUrlHelper)))
            {
                collection.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
                collection.TryAddScoped(serviceProvider =>
                {
                    var actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
                    var urlHelperFactory = serviceProvider.GetService<IUrlHelperFactory>();
                    return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
                });
            }
        }
    }
}
