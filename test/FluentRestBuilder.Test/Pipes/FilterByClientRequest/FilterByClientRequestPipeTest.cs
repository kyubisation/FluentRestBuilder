// <copyright file="FilterByClientRequestPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Pipes.FilterByClientRequest;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class FilterByClientRequestPipeTest : IDisposable
    {
        private readonly Interpreter filterInterpreter = new Interpreter();
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public FilterByClientRequestPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSource()
                .RegisterFilterByClientRequestPipe()
                .RegisterMappingPipe()
                .Services
                .AddScoped<IFilterByClientRequestInterpreter>(p => this.filterInterpreter)
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
            this.CreateFilterEntities();
            this.filterInterpreter.RequestedFilter.Add(
                new FilterRequest(nameof(Entity.Name), FilterType.Equals, "a"));
            var result = await this.controller.FromSource(this.database.Create().Entities)
                .ApplyFilterByClientRequest(
                    builder => builder
                        .AddFilter(nameof(Entity.Name), (f, b) => b.AddEquals(e => e.Name == f)))
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(2, result.Count);
            Assert.Contains(2, result.Select(e => e.Id));
            Assert.Contains(4, result.Select(e => e.Id));
        }

        [Fact]
        public async Task TestNotSupported()
        {
            this.CreateFilterEntities();
            this.filterInterpreter.RequestedFilter.Add(
                new FilterRequest(nameof(Entity.Name), FilterType.Equals, "a"));
            var result = await this.controller.FromSource(this.database.Create().Entities)
                .ApplyFilterByClientRequest(builder => builder)
                .Map(q => q.ToListAsync())
                .ToMockResultPipe()
                .Execute();
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        private void CreateFilterEntities()
        {
            using (var context = this.database.Create())
            {
                context.Add(new Entity { Id = 1, Name = "c" });
                context.Add(new Entity { Id = 2, Name = "a" });
                context.Add(new Entity { Id = 3, Name = "b" });
                context.Add(new Entity { Id = 4, Name = "a" });
                context.SaveChanges();
            }
        }

        private class Interpreter : IFilterByClientRequestInterpreter
        {
            public List<FilterRequest> RequestedFilter { get; } = new List<FilterRequest>();

            IEnumerable<FilterRequest> IFilterByClientRequestInterpreter.ParseRequestQuery() =>
                this.RequestedFilter;
        }
    }
}
