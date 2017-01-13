// <copyright file="SingleOrDefaultPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.SingleOrDefault
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Builder;
    using Common.Mocks;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SingleOrDefaultPipeTest : IDisposable
    {
        private readonly IServiceProvider provider;
        private readonly PersistantDatabase database;
        private MockDbContext context;

        public SingleOrDefaultPipeTest()
        {
            this.database = new PersistantDatabase();
            this.provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSingleOrDefaultPipe()
                .Services
                .BuildServiceProvider();
            this.context = this.database.Create();
        }

        [Fact]
        public async Task TestExistingEntity()
        {
            var entity = this.database.CreateSimilarEntities(2).First();
            var result = await new Source<IQueryable<Entity>>(this.context.Entities, this.provider)
                .SingleOrDefault(e => e.Id == entity.Id)
                .ToObjectResultOrDefault();
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task TestDuplicateEntity()
        {
            var entity = this.database.CreateSimilarEntities(2).First();
            var result = await new Source<IQueryable<Entity>>(this.context.Entities, this.provider)
                .SingleOrDefault(e => e.Name == entity.Name)
                .ToMockResultPipe()
                .Execute();
            Assert.IsType<StatusCodeResult>(result);
            var codeResult = (StatusCodeResult)result;
            Assert.Equal(StatusCodes.Status500InternalServerError, codeResult.StatusCode);
        }

        [Fact]
        public async Task TestMissingEntity()
        {
            var result = await new Source<IQueryable<Entity>>(this.context.Entities, this.provider)
                .SingleOrDefault(e => e.Id == 1)
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
