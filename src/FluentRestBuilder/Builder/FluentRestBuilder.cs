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
    using Pipes.OrderByClientRequest;
    using Pipes.OrderByClientRequest.Expressions;
    using Pipes.Queryable;
    using Pipes.SingleOrDefault;
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
            this.RegisterSourceFactories();
            this.RegisterPipeFactories();
            this.RegisterMappings();
            this.RegisterUtilities();
        }

        public IServiceCollection Services { get; }

        private void RegisterSourceFactories()
        {
            this.Services.TryAddScoped(
                typeof(ISourceFactory<>), typeof(SourceFactory<>));
            this.Services.TryAddScoped(
                typeof(ILazySourceFactory<>), typeof(LazySourceFactory<>));
        }

        private void RegisterPipeFactories()
        {
            this.Services.TryAddScoped(
                typeof(IActionPipeFactory<>), typeof(ActionPipeFactory<>));
            this.Services.TryAddScoped(
                typeof(IClaimValidationPipeFactory<>), typeof(ClaimValidationPipeFactory<>));
            this.Services.TryAddScoped(
                typeof(IEntityValidationPipeFactory<>), typeof(EntityValidationPipeFactory<>));
            this.Services.TryAddScoped(
                typeof(IMappingPipeFactory<,>), typeof(MappingPipeFactory<,>));
            this.Services.TryAddScoped(
                typeof(ICollectionMappingPipeFactory<,>), typeof(CollectionMappingPipeFactory<,>));
            this.Services.TryAddScoped(
                typeof(IValidationPipeFactory<>), typeof(ValidationPipeFactory<>));
            this.Services.TryAddScoped(
                typeof(IQueryablePipeFactory<,>), typeof(QueryablePipeFactory<,>));
            this.Services.TryAddScoped(
                typeof(ISingleOrDefaultPipeFactory<>), typeof(SingleOrDefaultPipeFactory<>));
            this.Services.TryAddScoped(
                typeof(IOrderByClientRequestPipeFactory<>),
                typeof(OrderByClientRequestPipeFactory<>));
        }

        private void RegisterMappings()
        {
            this.Services.TryAddScoped<IMapperFactory, MapperFactory>();
            this.Services.TryAddScoped(typeof(IMapperFactory<>), typeof(MapperFactory<>));
            this.Services.TryAddTransient(typeof(IMappingBuilder<>), typeof(MappingBuilder<>));
        }

        private void RegisterUtilities()
        {
            this.Services.TryAddScoped<IOrderByClientRequestInterpreter, OrderByClientRequestInterpreter>();
            this.Services.TryAddScoped(
                typeof(IOrderByExpressionBuilder<>), typeof(OrderByExpressionBuilder<>));
            this.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            this.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            this.Services.TryAddScoped(
                typeof(IAllowedOptionsBuilder<>), typeof(AllowedOptionsBuilder<>));
            this.Services.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));

            this.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            if (this.Services.Any(d => d.ServiceType == typeof(IUrlHelper)))
            {
                return;
            }

            this.RegisterUrlHelper();
        }

        private void RegisterUrlHelper()
        {
            this.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            this.Services.TryAddScoped(serviceProvider =>
            {
                var actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
                var urlHelperFactory = serviceProvider.GetService<IUrlHelperFactory>();
                return urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            });
        }
    }
}
