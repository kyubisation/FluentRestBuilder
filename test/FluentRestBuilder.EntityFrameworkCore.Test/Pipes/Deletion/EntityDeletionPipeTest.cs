// <copyright file="EntityDeletionPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Deletion
{
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.Pipes.Deletion;
    using FluentRestBuilder.Sources.Source;
    using FluentRestBuilder.Test.Common.Mocks;
    using FluentRestBuilder.Test.Common.Mocks.EntityFramework;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class EntityDeletionPipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestDeletion()
        {
            this.CreateEntities();
            var entity = Entity.Entities.First();
            var result = await new Source<Entity>(entity, this.ServiceProvider)
                .DeleteEntity()
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Id, result.Id);
            using (var context = this.CreateContext())
            {
                Assert.Equal(Entity.Entities.Count - 1, context.Entities.Count());
            }
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddScoped<IContextActions>(p => new ContextActions<MockDbContext>(this.Context));
            services.AddTransient<IEntityDeletionPipeFactory<Entity>, EntityDeletionPipeFactory<Entity>>();
        }
    }
}
