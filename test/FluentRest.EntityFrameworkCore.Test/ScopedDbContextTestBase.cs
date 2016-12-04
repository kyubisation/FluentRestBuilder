// <copyright file="ScopedDbContextTestBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Test
{
    using Common;
    using EntityFrameworkCore.MetaModel;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;

    public abstract class ScopedDbContextTestBase : TestBaseWithServiceProvider
    {
        private readonly DbContextOptions<MockDbContext> options;

        protected ScopedDbContextTestBase()
        {
            this.options = MockDbContext.ConfigureInMemoryContextOptions();
            this.Scope = this.ServiceProvider
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            this.Context = this.ResolveScoped<MockDbContext>();
            this.Factory = this.ResolveScoped<ExpressionFactory<Entity>>();
        }

        protected ExpressionFactory<Entity> Factory { get; }

        protected MockDbContext Context { get; }

        protected IServiceScope Scope { get; private set; }

        public override void Dispose()
        {
            this.Scope?.Dispose();
            this.Scope = null;

            base.Dispose();
        }

        protected override void Setup(IServiceCollection services)
        {
            services.AddScoped(s => new MockDbContext(this.options))
                .AddScoped<DbContext>(s => s.GetRequiredService<MockDbContext>())
                .AddScoped<IQueryableFactory, QueryableFactory>()
                .AddScoped(typeof(IQueryableFactory<>), typeof(QueryableFactory<>))
                .AddSingleton(typeof(IModelContainer<>), typeof(ModelContainer<>))
                .AddSingleton(typeof(ExpressionFactory<>));
        }

        protected MockDbContext CreateContext() => new MockDbContext(this.options);

        protected TService ResolveScoped<TService>() =>
            this.Scope.ServiceProvider.GetRequiredService<TService>();

        protected void CreateEntities() => MockDbContext.CreateEntities(this.options);

        protected void CreateMultiKeyEntities() =>
            MockDbContext.CreateMultiKeyEntities(this.options);
    }
}
