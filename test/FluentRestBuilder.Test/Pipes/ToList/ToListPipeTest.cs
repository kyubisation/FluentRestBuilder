// <copyright file="ToListPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.ToList
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

    public class ToListPipeTest : IDisposable
    {
        private readonly PersistantDatabase database;
        private readonly IServiceProvider provider;
        private MockDbContext context;

        public ToListPipeTest()
        {
            this.database = new PersistantDatabase();
            this.provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterToListPipe()
                .Services
                .BuildServiceProvider();
            this.context = this.database.Create();
        }

        [Fact]
        public async Task TestBasicUseCase()
        {
            var entities = this.database.CreateEnumeratedEntities(10);
            var result = await new Source<IQueryable<Entity>>(this.context.Entities, this.provider)
                .ToList()
                .ToObjectResultOrDefault();
            Assert.Equal(entities, result, new PropertyComparer<Entity>());
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
