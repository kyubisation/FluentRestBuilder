// <copyright file="ScopedDbContextTestBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test
{
    using EntityFrameworkCore.MetaModel;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.Extensions.DependencyInjection;
    using QueryableFactories;

    public abstract class ScopedDbContextTestBase : FluentRestBuilder.Test.Common.ScopedDbContextTestBase
    {
        protected ScopedDbContextTestBase()
        {
            this.Factory = this.ResolveScoped<ExpressionFactory<Entity>>();
        }

        protected ExpressionFactory<Entity> Factory { get; }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services
                .AddScoped<IQueryableFactory, ContextQueryableFactory<MockDbContext>>()
                .AddScoped(typeof(IQueryableFactory<>), typeof(QueryableFactory<>))
                .AddSingleton(typeof(IModelContainer<>), typeof(ModelContainer<>))
                .AddSingleton(typeof(ExpressionFactory<>));
        }
    }
}
