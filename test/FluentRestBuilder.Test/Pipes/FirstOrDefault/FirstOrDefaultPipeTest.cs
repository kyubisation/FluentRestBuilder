// <copyright file="FirstOrDefaultPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FirstOrDefault
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class FirstOrDefaultPipeTest : IDisposable
    {
        private readonly IServiceProvider provider;
        private readonly PersistantDatabase database;
        private MockDbContext context;

        public FirstOrDefaultPipeTest()
        {
            this.database = new PersistantDatabase();
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterFirstOrDefaultPipe()
                .Services
                .BuildServiceProvider();
            this.context = this.database.Create();
        }

        [Fact]
        public async Task TestExistingEntity()
        {
            var entity = this.database.CreateSimilarEntities(2).First();
            var result = await new Source<IQueryable<Entity>>(this.context.Entities, this.provider)
                .FirstOrDefault(e => e.Name == entity.Name)
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task TestMissingEntity()
        {
            var result = await new Source<IQueryable<Entity>>(this.context.Entities, this.provider)
                .FirstOrDefault(e => e.Id == 1)
                .ToObjectResultOrDefault();
            Assert.Null(result);
        }

        public void Dispose()
        {
            if (this.context == null)
            {
                return;
            }

            this.context.Dispose();
            this.context = null;
        }
    }
}
