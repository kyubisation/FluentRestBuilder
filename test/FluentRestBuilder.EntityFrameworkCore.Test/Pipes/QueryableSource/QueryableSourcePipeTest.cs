// <copyright file="QueryableSourcePipeTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Pipes.QueryableSource
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using EntityFrameworkCore.Pipes.QueryableSource;
    using FluentRestBuilder.Sources.Source;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class QueryableSourcePipeTest : ScopedDbContextTestBase
    {
        [Fact]
        public async Task TestCreationWithoutPredicate()
        {
            var parent = await this.CreateParentWithChildren();
            var parent2 = await this.CreateParentWithChildren();
            var result = await new Source<Parent>(parent, this.ServiceProvider)
                .SelectQueryableSource(f => f.Resolve<Child>())
                .ToObjectResultOrDefault();
            Assert.NotNull(result);
            Assert.Equal(
                parent.Children.Count + parent2.Children.Count,
                await result.CountAsync());
        }

        [Fact]
        public async Task TestCreationWithPredicate()
        {
            var parent = await this.CreateParentWithChildren();
            await this.CreateParentWithChildren();
            var result = await new Source<Parent>(parent, this.ServiceProvider)
                .SelectQueryableSource((f, p) => f.Resolve<Child>().Where(c => c.ParentId == p.Id))
                .ToObjectResultOrDefault();
            Assert.NotNull(result);
            Assert.Equal(parent.Children.Count, await result.CountAsync());
        }

        protected override void Setup(IServiceCollection services)
        {
            base.Setup(services);
            services.AddTransient<
                IQueryableSourcePipeFactory<Parent, Child>,
                QueryableSourcePipeFactory<Parent, Child>>();
            services.AddTransient<IQueryableFactory, ContextQueryableFactory<MockDbContext>>();
            services.AddTransient(typeof(IQueryableFactory<>), typeof(QueryableFactory<>));
        }

        private async Task<Parent> CreateParentWithChildren()
        {
            using (var context = this.CreateContext())
            {
                var id = await context.Parents.CountAsync() + 1;
                var parent = new Parent
                {
                    Id = id,
                    Name = "test",
                    Children = new List<Child>
                    {
                        new Child { Id = 1 + (id * 10), Name = "test1" },
                        new Child { Id = 2 + (id * 10), Name = "test2" }
                    }
                };
                context.Add(parent);
                await context.SaveChangesAsync();
                return parent;
            }
        }
    }
}
