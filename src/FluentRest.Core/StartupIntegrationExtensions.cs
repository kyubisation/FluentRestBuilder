namespace KyubiCode.FluentRest
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
    using MetaModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using RestCollectionMutators;
    using RestCollectionMutators.Filter;
    using RestCollectionMutators.OrderBy;
    using RestCollectionMutators.Pagination;
    using RestCollectionMutators.Search;
    using SourcePipes.EntityCollection;
    using SourcePipes.SelectableEntity;
    using Transformers;

    public static class StartupIntegrationExtensions
    {
        public static IMvcBuilder AddFluentRest<TContext>(this IMvcBuilder builder)
            where TContext : DbContext
        {
            builder.Services.AddFluentRest<TContext>();
            return builder;
        }

        public static IMvcCoreBuilder AddFluentRest<TContext>(this IMvcCoreBuilder builder)
            where TContext : DbContext
        {
            builder.Services.AddFluentRest<TContext>();
            return builder;
        }

        public static IServiceCollection AddFluentRest<TContext>(this IServiceCollection collection)
            where TContext : DbContext
        {
            RegisterDbContext<TContext>(collection);
            RegisterPipeFactories(collection);
            RegisterCollectionMutators(collection);
            RegisterTransformations(collection);
            RegisterUtilities(collection);

            return collection;
        }

        private static void RegisterDbContext<TContext>(IServiceCollection collection)
            where TContext : DbContext
        {
            collection.TryAddScoped<DbContext>(r => r.GetRequiredService<TContext>());
        }

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

        private static void RegisterCollectionMutators(IServiceCollection collection)
        {
            collection.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
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
            collection.TryAddSingleton(typeof(IModelContainer<>), typeof(ModelContainer<>));
            collection.TryAddSingleton(typeof(IExpressionFactory<>), typeof(ExpressionFactory<>));
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
