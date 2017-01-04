// <copyright file="EntityUpdatePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.Update
{
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.Pipes.Update;
    using FluentRestBuilder.Sources.Source;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;
    using Xunit;

    public class EntityUpdatePipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestUpdate()
        {
            const string newName = "TestUpdate";
            this.CreateEntities();
            var entity = this.Context.Set<Entity>().First();
            Assert.NotEqual(newName, entity.Name);
            entity.Name = newName;

            var result = await new Source<Entity>(entity, this.ServiceProvider)
                .UpdateEntity()
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Id, result.Id);
            using (var context = this.CreateContext())
            {
                var updatedEntity = context.Entities.Single(e => e.Id == entity.Id);
                Assert.Equal(newName, updatedEntity.Name);
            }
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddTransient<IEntityUpdatePipeFactory<Entity>>(
                p => new EntityUpdatePipeFactory<Entity>(
                    new ContextActions<MockDbContext>(this.Context)));
            services.AddTransient<IScopedStorage<Entity>, ScopedStorage<Entity>>();
        }
    }
}
