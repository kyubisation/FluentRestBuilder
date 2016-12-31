// <copyright file="FluentRestBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using System;
    using Hal;
    using Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;
    using Pipes.Actions;
    using Pipes.ClaimValidation;
    using Pipes.CollectionMapping;
    using Pipes.EntityValidation;
    using Pipes.FilterByClientRequest;
    using Pipes.FilterByClientRequest.Expressions;
    using Pipes.Mapping;
    using Pipes.OrderByClientRequest;
    using Pipes.OrderByClientRequest.Expressions;
    using Pipes.PaginationByClientRequest;
    using Pipes.Queryable;
    using Pipes.SearchByClientRequest;
    using Pipes.SingleOrDefault;
    using Pipes.Validation;
    using Results.Options;
    using Storage;

    public class FluentRestBuilder : IFluentRestBuilder
    {
        public FluentRestBuilder(IServiceCollection services)
        {
            this.Services = services;
            this.RegisterPipeFactories();
            this.RegisterMappings();
            this.RegisterInterpreters();
            this.RegisterStorage();
            this.RegisterUtilities();
        }

        public IServiceCollection Services { get; }

        public IFluentRestBuilder AddRestMapper<TInput, TOutput>(
            Func<TInput, TOutput> mapping,
            Action<RestMapper<TInput, TOutput>> configuration = null)
            where TOutput : RestEntity
        {
            this.Services.AddScoped<IMapper<TInput, TOutput>>(
                serviceProvider =>
                {
                    var urlHelper = serviceProvider.GetService<IScopedStorage<IUrlHelper>>()?.Value
                        ?? serviceProvider.GetService<IUrlHelper>();
                    var mapper = new RestMapper<TInput, TOutput>(mapping, urlHelper);
                    configuration?.Invoke(mapper);
                    return mapper;
                });
            return this;
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
            this.Services.TryAddScoped(
                typeof(ISearchByClientRequestPipeFactory<>),
                typeof(SearchByClientRequestPipeFactory<>));
            this.Services.TryAddScoped(
                typeof(IFilterByClientRequestPipeFactory<>),
                typeof(FilterByClientRequestPipeFactory<>));
            this.Services.TryAddScoped(
                typeof(IPaginationByClientRequestPipeFactory<>),
                typeof(PaginationByClientRequestPipeFactory<>));
        }

        private void RegisterMappings()
        {
            this.Services.TryAddScoped<IMapperFactory, MapperFactory>();
            this.Services.TryAddScoped(typeof(IMapperFactory<>), typeof(MapperFactory<>));
            this.Services.TryAddTransient(typeof(IMappingBuilder<>), typeof(MappingBuilder<>));
        }

        private void RegisterInterpreters()
        {
            this.Services.TryAddScoped<
                IOrderByClientRequestInterpreter,
                OrderByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                IFilterByClientRequestInterpreter,
                FilterByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                IPaginationByClientRequestInterpreter,
                PaginationByClientRequestInterpreter>();
        }

        private void RegisterStorage()
        {
            this.Services.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
            this.Services.TryAddScoped(this.RegisterHttpContextScopedStorage);
            this.Services.TryAddScoped(this.RegisterUrlHelperScopedStorage);
        }

        private void RegisterUtilities()
        {
            this.Services.TryAddScoped(
                typeof(IOrderByExpressionBuilder<>), typeof(OrderByExpressionBuilder<>));
            this.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            this.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            this.Services.TryAddScoped(
                typeof(IAllowedOptionsBuilder<>), typeof(AllowedOptionsBuilder<>));
            this.Services.TryAddTransient(
                typeof(IFilterExpressionProviderBuilder<>),
                typeof(FilterExpressionProviderBuilder<>));
            this.Services.TryAddScoped(
                typeof(IFilterExpressionBuilder<>), typeof(FilterExpressionBuilder<>));
            this.Services.TryAddScoped<IRestCollectionLinkGenerator, RestCollectionLinkGenerator>();
            this.Services.TryAddSingleton<IHttpVerbMap, HttpVerbMap>();
        }

        private IScopedStorage<HttpContext> RegisterHttpContextScopedStorage(
            IServiceProvider serviceProvider)
        {
            var accessor = serviceProvider.GetService<IHttpContextAccessor>();
            return new ScopedStorage<HttpContext> { Value = accessor?.HttpContext };
        }

        private IScopedStorage<IUrlHelper> RegisterUrlHelperScopedStorage(
            IServiceProvider serviceProvider)
        {
            var actionContextAccessor = serviceProvider.GetService<IActionContextAccessor>();
            if (actionContextAccessor == null)
            {
                return new ScopedStorage<IUrlHelper>();
            }

            var urlHelperFactory = serviceProvider.GetService<IUrlHelperFactory>();
            var urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
            return new ScopedStorage<IUrlHelper>
            {
                Value = urlHelper
            };
        }
    }
}
