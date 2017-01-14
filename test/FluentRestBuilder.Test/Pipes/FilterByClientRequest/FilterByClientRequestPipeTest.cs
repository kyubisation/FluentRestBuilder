// <copyright file="FilterByClientRequestPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.Mocks;
    using Common.Mocks.EntityFramework;
    using FluentRestBuilder.Pipes.FilterByClientRequest;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Expressions;
    using FluentRestBuilder.Pipes.Mapping;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class FilterByClientRequestPipeTest : ScopedDbContextTestBase
    {
        private readonly Interpreter filterInterpreter = new Interpreter();

        [Fact]
        public async Task TestBasicUseCase()
        {
            this.CreateFilterEntities();
            this.filterInterpreter.RequestedFilter.Add(
                new FilterRequest(nameof(Entity.Name), FilterType.Equals, "a"));
            var result = await new Source<IQueryable<Entity>>(
                    this.Context.Entities, this.ServiceProvider)
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
            var result = await new Source<IQueryable<Entity>>(
                    this.Context.Entities, this.ServiceProvider)
                .ApplyFilterByClientRequest(builder => builder)
                .Map(q => q.ToListAsync())
                .ToMockResultPipe()
                .Execute();
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddTransient<IFilterExpressionBuilder<Entity>, FilterExpressionBuilder<Entity>>();
            services.AddTransient<
                IFilterExpressionProviderBuilder<Entity>,
                FilterExpressionProviderBuilder<Entity>>();
            services.AddScoped<IFilterByClientRequestPipeFactory<Entity>>(
                p => new FilterByClientRequestPipeFactory<Entity>(this.filterInterpreter));
            services.AddScoped<
                IMappingPipeFactory<IQueryable<Entity>, List<Entity>>,
                MappingPipeFactory<IQueryable<Entity>, List<Entity>>>();
        }

        private void CreateFilterEntities()
        {
            using (var context = this.CreateContext())
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
