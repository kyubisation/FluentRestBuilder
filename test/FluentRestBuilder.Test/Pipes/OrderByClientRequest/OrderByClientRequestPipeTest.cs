// <copyright file="OrderByClientRequestPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.OrderByClientRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Builder;
    using FluentRestBuilder.Pipes.OrderByClientRequest;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Mocks.EntityFramework;
    using Xunit;

    public class OrderByClientRequestPipeTest : IDisposable
    {
        private readonly Interpreter orderByInterpreter = new Interpreter();
        private readonly PersistantDatabase database;
        private readonly MockController controller;

        public OrderByClientRequestPipeTest()
        {
            this.database = new PersistantDatabase();
            var provider = new FluentRestBuilderCore(new ServiceCollection())
                .RegisterSource()
                .RegisterQueryablePipe()
                .RegisterOrderByClientRequestPipe()
                .RegisterMappingPipe()
                .Services
                .AddScoped<IOrderByClientRequestInterpreter>(p => this.orderByInterpreter)
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
            this.orderByInterpreter.RequestedOrderBy
                .Add(new OrderByRequest(nameof(Entity.Name), OrderByDirection.Ascending));
            this.CreateOrderByEntities();
            var result = await this.controller.FromSource(this.database.Create().Entities)
                .ApplyOrderByClientRequest(b => b.Add(nameof(Entity.Name), e => e.Name))
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { "a", "b", "c" }, result.Select(e => e.Name));
        }

        [Fact]
        public async Task TestDefaultOrderBy()
        {
            this.CreateOrderByEntities();

            var result = await this.controller.FromSource(this.database.Create().Entities)
                .OrderByDescending(e => e.Name)
                .ApplyOrderByClientRequest(b => b.Add(nameof(Entity.Name), e => e.Name))
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { "c", "b", "a" }, result.Select(e => e.Name));
        }

        [Fact]
        public async Task TestDefaultOrderByOverridden()
        {
            this.orderByInterpreter.RequestedOrderBy
                .Add(new OrderByRequest(nameof(Entity.Name), OrderByDirection.Ascending));
            this.CreateOrderByEntities();

            var result = await this.controller.FromSource(this.database.Create().Entities)
                .OrderByDescending(e => e.Name)
                .ApplyOrderByClientRequest(b => b.Add(nameof(Entity.Name), e => e.Name))
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { "a", "b", "c" }, result.Select(e => e.Name));
        }

        private void CreateOrderByEntities()
        {
            using (var context = this.database.Create())
            {
                context.Add(new Entity { Id = 1, Name = "c" });
                context.Add(new Entity { Id = 2, Name = "a" });
                context.Add(new Entity { Id = 3, Name = "b" });
                context.SaveChanges();
            }
        }

        private class Interpreter : IOrderByClientRequestInterpreter
        {
            public List<OrderByRequest> RequestedOrderBy { get; } = new List<OrderByRequest>();

            public IEnumerable<OrderByRequest> ParseRequestQuery(
                ICollection<string> supportedOrderBys) =>
                this.RequestedOrderBy;
        }
    }
}
