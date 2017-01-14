// <copyright file="OrderByClientRequestPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Pipes.Mapping;
    using FluentRestBuilder.Pipes.OrderByClientRequest;
    using FluentRestBuilder.Pipes.OrderByClientRequest.Expressions;
    using FluentRestBuilder.Pipes.Queryable;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class OrderByClientRequestPipeTest : ScopedDbContextTestBase
    {
        private readonly Interpreter orderByInterpreter = new Interpreter();

        [Fact]
        public async Task TestBasicUseCase()
        {
            this.orderByInterpreter.RequestedOrderBy
                .Add(new OrderByRequest(nameof(Entity.Name), OrderByDirection.Ascending));
            this.CreateOrderByEntities();

            var result = await new Source<IQueryable<Entity>>(
                    this.Context.Entities, this.ServiceProvider)
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

            var result = await new Source<IQueryable<Entity>>(
                    this.Context.Entities, this.ServiceProvider)
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

            var result = await new Source<IQueryable<Entity>>(
                    this.Context.Entities, this.ServiceProvider)
                .OrderByDescending(e => e.Name)
                .ApplyOrderByClientRequest(b => b.Add(nameof(Entity.Name), e => e.Name))
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { "a", "b", "c" }, result.Select(e => e.Name));
        }

        [Fact]
        public async Task TestNotSupported()
        {
            this.orderByInterpreter.RequestedOrderBy
                .Add(new OrderByRequest(nameof(Entity.Name), OrderByDirection.Ascending));
            this.CreateOrderByEntities();

            var result = await new Source<IQueryable<Entity>>(
                    this.Context.Entities, this.ServiceProvider)
                .ApplyOrderByClientRequest(b => b)
                .Map(q => q.ToListAsync())
                .ToMockResultPipe()
                .Execute();
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddTransient<IOrderByExpressionBuilder<Entity>, OrderByExpressionBuilder<Entity>>();
            services.AddScoped<IOrderByClientRequestPipeFactory<Entity>>(
                p => new OrderByClientRequestPipeFactory<Entity>(this.orderByInterpreter));
            services.AddScoped<
                IMappingPipeFactory<IQueryable<Entity>, List<Entity>>,
                MappingPipeFactory<IQueryable<Entity>, List<Entity>>>();
            services.AddScoped<
                IQueryablePipeFactory<IQueryable<Entity>, IOrderedQueryable<Entity>>,
                QueryablePipeFactory<IQueryable<Entity>, IOrderedQueryable<Entity>>>();
        }

        private void CreateOrderByEntities()
        {
            using (var context = this.CreateContext())
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

            public IEnumerable<OrderByRequest> ParseRequestQuery() =>
                this.RequestedOrderBy;
        }
    }
}
