// <copyright file="FluentRestBuilderExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;
    using EntityFrameworkCore;
    using EntityFrameworkCore.MetaModel;
    using EntityFrameworkCore.Pipes;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;

    public static class FluentRestBuilderExtension
    {
        public static IFluentRestBuilder AddEntityFrameworkCorePipes<TContext>(
            this IFluentRestBuilder builder)
            where TContext : DbContext
        {
            RegisterEntityFrameworkRelatedServices<TContext>(builder.Services);

            builder.Services.AddSingleton(
                typeof(IQueryableTransformer<>), typeof(AsyncQueryableTransformer<>));
            return builder.RegisterDeletionPipe()
                .RegisterInsertionPipe()
                .RegisterQueryableSourcePipe()
                .RegisterUpdatePipe();
        }

        private static void RegisterEntityFrameworkRelatedServices<TContext>(
            IServiceCollection collection)
            where TContext : DbContext
        {
            collection.TryAddScoped<IQueryableFactory, ContextQueryableFactory<TContext>>();
            collection.TryAddScoped(typeof(IQueryableFactory<>), typeof(QueryableFactory<>));
            collection.TryAddSingleton(typeof(IModelContainer<>), typeof(ModelContainer<>));
        }
    }
}
