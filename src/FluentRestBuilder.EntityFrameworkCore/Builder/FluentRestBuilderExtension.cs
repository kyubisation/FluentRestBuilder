// <copyright file="FluentRestBuilderExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using Common;
    using EntityFrameworkCore.Common;
    using EntityFrameworkCore.MetaModel;
    using EntityFrameworkCore.Pipes.Deletion;
    using EntityFrameworkCore.Pipes.EntityCollectionSource;
    using EntityFrameworkCore.Pipes.Insertion;
    using EntityFrameworkCore.Pipes.Update;
    using EntityFrameworkCore.RestCollectionMutators;
    using EntityFrameworkCore.RestCollectionMutators.Filter;
    using EntityFrameworkCore.RestCollectionMutators.OrderBy;
    using EntityFrameworkCore.RestCollectionMutators.Pagination;
    using EntityFrameworkCore.RestCollectionMutators.Search;
    using EntityFrameworkCore.Sources.EntityCollection;
    using EntityFrameworkCore.Sources.QueryableSource;
    using EntityFrameworkCore.Sources.SelectableEntity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.CollectionMapping;

    public static class FluentRestBuilderExtension
    {
        public static IFluentRestBuilder AddEntityFrameworkCorePipes<TContext>(
            this IFluentRestBuilder builder)
            where TContext : DbContext
        {
            RegisterEntityFrameworkRelatedServices<TContext>(builder.Services);
            RegisterSources(builder.Services);
            RegisterPipes(builder.Services);
            RegisterCollectionServices(builder.Services);

            return builder;
        }

        private static void RegisterEntityFrameworkRelatedServices<TContext>(
            IServiceCollection collection)
            where TContext : DbContext
        {
            collection.TryAddScoped<IQueryableFactory, ContextQueryableFactory<TContext>>();
            collection.TryAddScoped(typeof(IQueryableFactory<>), typeof(QueryableFactory<>));
            collection.TryAddSingleton(typeof(IModelContainer<>), typeof(ModelContainer<>));
        }

        private static void RegisterSources(IServiceCollection collection)
        {
            collection.TryAddScoped(
                typeof(IEntityCollectionSourceFactory<>), typeof(EntityCollectionSourceFactory<>));
            collection.TryAddScoped(
                typeof(ISelectableEntitySourceFactory<>), typeof(SelectableEntitySourceFactory<>));
            collection.TryAddScoped(
                typeof(IQueryableSourceFactory<>), typeof(QueryableSourceFactory<>));
        }

        private static void RegisterPipes(IServiceCollection collection)
        {
            collection.TryAddScoped(
                typeof(IEntityInsertionPipeFactory<>), typeof(EntityInsertionPipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityUpdatePipeFactory<>), typeof(EntityUpdatePipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityDeletionPipeFactory<>), typeof(EntityDeletionPipeFactory<>));
            collection.TryAddScoped(
                typeof(IEntityCollectionSourcePipeFactory<,>),
                typeof(EntityCollectionSourcePipeFactory<,>));
        }

        private static void RegisterCollectionServices(IServiceCollection collection)
        {
            collection.TryAddScoped<IRestCollectionLinkGenerator, RestCollectionLinkGenerator>();
            collection.TryAddScoped(
                typeof(IRestCollectionFilter<>), typeof(RestCollectionFilter<>));
            collection.TryAddScoped(
                typeof(IRestCollectionSearch<>), typeof(RestCollectionSearch<>));
            collection.TryAddScoped(
                typeof(IRestCollectionOrderBy<>), typeof(RestCollectionOrderBy<>));
            collection.TryAddScoped(
                typeof(IRestCollectionPagination<>), typeof(RestCollectionPagination<>));
        }
    }
}
