// <copyright file="SearchByClientRequestPipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.SearchByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.Mocks;
    using FluentRestBuilder.Pipes.Mapping;
    using FluentRestBuilder.Pipes.Queryable;
    using FluentRestBuilder.Pipes.SearchByClientRequest;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class SearchByClientRequestPipeTest : ScopedDbContextTestBase
    {
        private readonly Interpreter interpreter = new Interpreter();

        [Fact]
        public async Task TestBasicUseCase()
        {
            this.CreateOrderByEntities();
            this.interpreter.SearchValue = "b";

            var result = await new Source<IQueryable<Entity>>(
                    this.Context.Entities, this.ServiceProvider)
                .ApplySearchByClientRequest(s => e => e.Name.Contains(s))
                .Map(q => q.ToListAsync())
                .ToObjectResultOrDefault();
            Assert.Equal(2, result.Count);
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddTransient<
                ISearchByClientRequestPipeFactory<Entity>,
                SearchByClientRequestPipeFactory<Entity>>();
            services.AddScoped<
                IMappingPipeFactory<IQueryable<Entity>, List<Entity>>,
                MappingPipeFactory<IQueryable<Entity>, List<Entity>>>();
            services.AddScoped<
                IQueryablePipeFactory<IQueryable<Entity>, IOrderedQueryable<Entity>>,
                QueryablePipeFactory<IQueryable<Entity>, IOrderedQueryable<Entity>>>();

            services.AddSingleton<ISearchByClientRequestInterpreter>(p => this.interpreter);
        }

        private void CreateOrderByEntities()
        {
            using (var context = this.CreateContext())
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
            public string SearchValue { get; set; }

            public string ParseRequestQuery() => this.SearchValue;
        }
    }
}
