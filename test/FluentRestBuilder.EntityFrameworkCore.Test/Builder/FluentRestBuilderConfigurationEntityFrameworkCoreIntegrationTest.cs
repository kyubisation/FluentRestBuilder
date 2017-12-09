// <copyright file="FluentRestBuilderConfigurationEntityFrameworkCoreIntegrationTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.Builder
{
    using FluentRestBuilder.Operators.ClientRequest.FilterExpressions;
    using FluentRestBuilder.Operators.ClientRequest.OrderByExpressions;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks.EntityFramework;
    using Xunit;

    public class FluentRestBuilderConfigurationEntityFrameworkCoreIntegrationTest
    {
        [Fact]
        public void TestConfiguration()
        {
            var provider = new ServiceCollection()
                .AddFluentRestBuilder()
                .ConfigureFiltersAndOrderByExpressionsForDbContextEntities<MockDbContext>()
                .Services
                .BuildServiceProvider();
            Assert.NotNull(provider.GetService<IFilterExpressionProviderDictionary<Entity>>());
            Assert.NotNull(provider.GetService<IOrderByExpressionDictionary<Entity>>());
            Assert.NotNull(provider.GetService<IFilterExpressionProviderDictionary<MultiKeyEntity>>());
            Assert.NotNull(provider.GetService<IOrderByExpressionDictionary<MultiKeyEntity>>());
            Assert.NotNull(provider.GetService<IFilterExpressionProviderDictionary<Parent>>());
            Assert.NotNull(provider.GetService<IOrderByExpressionDictionary<Parent>>());
            Assert.NotNull(provider.GetService<IFilterExpressionProviderDictionary<Child>>());
            Assert.NotNull(provider.GetService<IOrderByExpressionDictionary<Child>>());
            Assert.NotNull(provider.GetService<IFilterExpressionProviderDictionary<OtherEntity>>());
            Assert.NotNull(provider.GetService<IOrderByExpressionDictionary<OtherEntity>>());
        }
    }
}
