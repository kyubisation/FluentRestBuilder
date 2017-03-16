// <copyright file="PaginationByClientRequestPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.PaginationByClientRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Pipes.PaginationByClientRequest;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class PaginationByClientRequestPipeTest : IDisposable
    {
        private readonly Interpreter interpreter = new Interpreter();
        private readonly PersistantDatabase database;
        private readonly MockController controller;
        private MockDbContext context;

        public PaginationByClientRequestPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCoreConfiguration(new ServiceCollection())
                .RegisterSource()
                .RegisterPaginationByClientRequestPipe()
                .RegisterMappingPipe()
                .Services
                .AddScoped<IPaginationByClientRequestInterpreter>(p => this.interpreter)
                .BuildServiceProvider();
            this.controller = new MockController(provider);
            this.context = this.database.Create();
        }

        public void Dispose()
        {
            if (this.context == null)
            {
                return;
            }

            this.controller.Dispose();
            this.context.Dispose();
            this.context = null;
        }

        [Fact]
        public async Task TestDefaultCase()
        {
            var amount = new PaginationOptions().DefaultLimit;
            var entities = this.database.CreateEnumeratedEntities(20).Take(amount);
            await this.BuildPipeTestAndAssertResult(entities);
        }

        [Fact]
        public async Task TestChangedDefaultEntriesPerPageAmount()
        {
            const int amount = 5;
            var options = new PaginationOptions { DefaultLimit = amount };
            var entities = this.database.CreateEnumeratedEntities(20)
                .Take(amount);
            await this.BuildPipeTestAndAssertResult(entities, options);
        }

        [Fact]
        public void TestInvalidOptions()
        {
            var options = new PaginationOptions
            {
                MaxLimit = 5,
                DefaultLimit = 10
            };
            var source = this.controller.FromSource(this.context.Entities);
            Assert.Throws<InvalidOperationException>(
                () => source.ApplyPaginationByClientRequest(options));
        }

        [Fact]
        public async Task TestSecondPage()
        {
            var amount = new PaginationOptions().DefaultLimit;
            var entities = this.database.CreateEnumeratedEntities(20)
                .Skip(amount)
                .Take(amount);
            this.interpreter.Offset = 2;
            await this.BuildPipeTestAndAssertResult(entities);
        }

        [Fact]
        public async Task TestRequestedEntriesPerPage()
        {
            const int entriesPerPage = 5;
            this.interpreter.Limit = entriesPerPage;
            var entities = this.database.CreateEnumeratedEntities(20)
                .Take(entriesPerPage);
            await this.BuildPipeTestAndAssertResult(entities);
        }

        [Fact]
        public async Task TestRequestedEntriesPerPageExceedsMaximum()
        {
            const int maxLimit = 10;
            this.interpreter.Limit = 20;
            this.database.CreateEnumeratedEntities(20);
            var result = await this.controller.FromSource(this.context.Entities)
                .ApplyPaginationByClientRequest(new PaginationOptions { MaxLimit = maxLimit })
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(maxLimit, result.Count);
        }

        private async Task BuildPipeTestAndAssertResult(
            IEnumerable<Entity> entities, PaginationOptions options = null)
        {
            var entityList = entities.ToList();
            var result = await this.BuildPipeTest(options);
            Assert.Equal(entityList.Count, result.Count);
            Assert.Equal(entityList, result, new PropertyComparer<Entity>());
        }

        private Task<List<Entity>> BuildPipeTest(PaginationOptions options = null) =>
            this.controller.FromSource(this.context.Entities)
                .ApplyPaginationByClientRequest(options)
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();

        private class Interpreter : IPaginationByClientRequestInterpreter
        {
            public int? Offset { private get; set; }

            public int? Limit { private get; set; }

            public PaginationRequest ParseRequestQuery()
            {
                return new PaginationRequest(this.Offset, this.Limit);
            }
        }
    }
}
