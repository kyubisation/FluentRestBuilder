// <copyright file="SingleOrDefaultIntegrationTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.SingleOrDefault
{
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.MetaModel;
    using FluentRestBuilder.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class SingleOrDefaultIntegrationTest
    {
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public SingleOrDefaultIntegrationTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterStorage()
                .RegisterSingleOrDefaultPipe()
                .RegisterContext<MockDbContext>()
                .RegisterQueryableSource()
                .Services
                .AddScoped(p => this.database.Create())
                .BuildServiceProvider();
            this.controller = new MockController(provider);
        }

        [Fact]
        public async Task TestParamsResolution()
        {
            var entity = this.database.CreateEnumeratedEntities(3).First();
            var result = await this.controller.WithQueryable<Entity>()
                .SingleOrDefault(entity.Id)
                .ToObjectResultOrDefault();
            Assert.Equal(entity, result, new PropertyComparer<Entity>());
        }

        [Fact]
        public void TestKeyMismatch()
        {
            var pipe = this.controller.WithQueryable<Entity>();
            Assert.Throws<PrimaryKeyMismatchException>(() => pipe.SingleOrDefault(1, 2));
        }
    }
}
