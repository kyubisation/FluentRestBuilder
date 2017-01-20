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
    using Microsoft.AspNetCore.Mvc;
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
            var provider = new FluentRestBuilderCore(new ServiceCollection())
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
            var amount = new PaginationOptions().DefaultEntriesPerPage;
            var entities = this.database.CreateEnumeratedEntities(20).Take(amount);
            await this.BuildPipeTestAndAssertResult(entities);
        }

        [Fact]
        public async Task TestChangedDefaultEntriesPerPageAmount()
        {
            const int amount = 5;
            var options = new PaginationOptions { DefaultEntriesPerPage = amount };
            var entities = this.database.CreateEnumeratedEntities(20)
                .Take(amount);
            await this.BuildPipeTestAndAssertResult(entities, options);
        }

        [Fact]
        public void TestInvalidOptions()
        {
            var options = new PaginationOptions
            {
                MaxEntriesPerPage = 5,
                DefaultEntriesPerPage = 10
            };
            var source = this.controller.FromSource(this.context.Entities);
            Assert.Throws<InvalidOperationException>(
                () => source.ApplyPaginationByClientRequest(options));
        }

        [Fact]
        public async Task TestSecondPage()
        {
            var amount = new PaginationOptions().DefaultEntriesPerPage;
            var entities = this.database.CreateEnumeratedEntities(20)
                .Skip(amount)
                .Take(amount);
            this.interpreter.Page = 2;
            await this.BuildPipeTestAndAssertResult(entities);
        }

        [Fact]
        public async Task TestRequestedEntriesPerPage()
        {
            const int entriesPerPage = 5;
            this.interpreter.EntriesPerPage = entriesPerPage;
            var entities = this.database.CreateEnumeratedEntities(20)
                .Take(entriesPerPage);
            await this.BuildPipeTestAndAssertResult(entities);
        }

        [Fact]
        public async Task TestRequestedEntriesPerPageExceedsMaximum()
        {
            this.interpreter.EntriesPerPage = 20;
            var result = await this.controller.FromSource(this.context.Entities)
                .ApplyPaginationByClientRequest(new PaginationOptions { MaxEntriesPerPage = 10 })
                .Map(q => q.ToListAsync())
                .ToMockResultPipe()
                .Execute();
            Assert.IsType<BadRequestObjectResult>(result);
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
            public int? Page { get; set; }

            public int? EntriesPerPage { get; set; }

            public PaginationRequest ParseRequestQuery()
            {
                return new PaginationRequest(this.Page, this.EntriesPerPage);
            }
        }
    }
}
