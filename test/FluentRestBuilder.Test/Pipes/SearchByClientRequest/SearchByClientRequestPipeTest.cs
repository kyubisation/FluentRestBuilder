// <copyright file="SearchByClientRequestPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.SearchByClientRequest
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Pipes.SearchByClientRequest;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SearchByClientRequestPipeTest : IDisposable
    {
        private readonly Interpreter interpreter = new Interpreter();
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public SearchByClientRequestPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterStorage()
                .RegisterSource()
                .RegisterSearchByClientRequestPipe()
                .RegisterMappingPipe()
                .Services
                .AddScoped<ISearchByClientRequestInterpreter>(p => this.interpreter)
                .BuildServiceProvider();
            this.controller = new MockController(provider);
        }

        public void Dispose()
        {
            this.controller.Dispose();
        }

        [Fact]
        public async Task TestBasicUseCase()
        {
            this.CreateOrderByEntities();
            this.interpreter.SearchValue = "b";

            var result = await this.controller.FromSource(this.database.Create().Entities)
                .ApplySearchByClientRequest(s => e => e.Name.Contains(s))
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(2, result.Count);
        }

        private void CreateOrderByEntities()
        {
            using (var context = this.database.Create())
            {
                context.Add(new Entity { Id = 1, Name = "ac" });
                context.Add(new Entity { Id = 2, Name = "aa" });
                context.Add(new Entity { Id = 3, Name = "ab" });
                context.Add(new Entity { Id = 4, Name = "ab" });
                context.SaveChanges();
            }
        }

        private class Interpreter : ISearchByClientRequestInterpreter
        {
            public string SearchValue { private get; set; }

            public string ParseRequestQuery() => this.SearchValue;
        }
    }
}
