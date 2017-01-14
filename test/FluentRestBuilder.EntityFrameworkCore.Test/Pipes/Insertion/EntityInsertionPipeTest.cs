// <copyright file="EntityInsertionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Insertion
{
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.Pipes.Insertion;
    using FluentRestBuilder.Sources.Source;
    using FluentRestBuilder.Test.Common.Mocks;
    using FluentRestBuilder.Test.Common.Mocks.EntityFramework;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;

    public class EntityInsertionPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestInsertion()
        {
            var entity = new Entity { Id = 1, Name = "test" };
            var result = await new Source<Entity>(entity, this.ServiceProvider)
                .InsertEntity()
                .ToObjectResultOrDefault();
            Assert.Same(entity, result);
            using (var context = this.CreateContext())
            {
                Assert.Equal(1, context.Entities.Count(e => e.Id == entity.Id));
            }
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddScoped<IContextActions>(p => new ContextActions<MockDbContext>(this.Context));
            services.AddTransient<IScopedStorage<Entity>, ScopedStorage<Entity>>();
            services.AddTransient<IEntityInsertionPipeFactory<Entity>, EntityInsertionPipeFactory<Entity>>();
        }
    }
}
