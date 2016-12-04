// <copyright file="FluentRestBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using System.Linq;
    using ChainPipes.ClaimValidation;
    using ChainPipes.CollectionTransformation;
    using ChainPipes.Deletion;
    using ChainPipes.EntityCollectionSource;
    using ChainPipes.Insertion;
    using ChainPipes.Transformation;
    using ChainPipes.Update;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using SourcePipes.EntityCollection;
    using SourcePipes.SelectableEntity;
    using Transformers;

    public class FluentRestBuilder : IFluentRestBuilder
    {
        public FluentRestBuilder(IServiceCollection services)
        {
            this.Services = services;
            RegisterPipeFactories(this.Services);
            RegisterTransformations(this.Services);
            RegisterUtilities(this.Services);
        }

        public IServiceCollection Services { get; }

        private static void RegisterPipeFactories(IServiceCollection collection)
        {
            collection.TryAddScoped(
                typeof(IEntityCollectionSourceFactory<>), typeof(EntityCollectionSourceFactory<>));
            collection.TryAddScoped(
                typeof(ISelectableEntitySourceFactory<>), typeof(SelectableEntitySourceFactory<>));
            collection.TryAddScoped(
                typeof(IClaimValidationPipeFactory<>), typeof(ClaimValidationPipeFactory<>));
            collection.TryAddScoped(
                typeof(ICollectionTransformationPipeFactory<,>),
                typeof(CollectionTransformationPipeFactory<,>));
            collection.TryAddScoped(
                typeof(IEntityDeletionPipeFactory<>), typeof(EntityDeletionPipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityCollectionSourcePipeFactory<,>),
                typeof(EntityCollectionSourcePipeFactory<,>));
            collection.TryAddScoped(
                typeof(IEntityInsertionPipeFactory<>), typeof(EntityInsertionPipeFactory<>));
            collection.TryAddScoped(
                typeof(ITransformationPipeFactory<,>), typeof(TransformationPipeFactory<,>));
            collection.TryAddScoped(
                typeof(IEntityUpdatePipeFactory<>), typeof(EntityUpdatePipeFactory<>));
        }

        private static void RegisterTransformations(IServiceCollection collection)
        {
            collection.TryAddScoped<ITransformerFactory, TransformerFactory>();
            collection.TryAddScoped(typeof(ITransformerFactory<>), typeof(TransformerFactory<>));
            collection.TryAddTransient(typeof(ITransformationBuilder<>), typeof(TransformationBuilder<>));
        }

        private static void RegisterUtilities(IServiceCollection collection)
        {
            collection.TryAddScoped<IQueryableFactory, QueryableFactory>();
            collection.TryAddScoped(typeof(IQueryableFactory<>), typeof(QueryableFactory<>));
            collection.TryAddScoped(
                s => s.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Query);
            collection.TryAddTransient(typeof(LazyResolver<>));

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
