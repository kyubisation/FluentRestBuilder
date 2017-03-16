// <copyright file="GenericFilterExpressionProviderTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Pipes.FilterByClientRequest.Expressions
{
    using System.Linq;
    using System.Linq.Expressions;
    using FluentRestBuilder.Pipes.FilterByClientRequest;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Converters;
    using FluentRestBuilder.Pipes.FilterByClientRequest.Expressions;
    using Mocks.EntityFramework;
    using Xunit;

    public class GenericFilterExpressionProviderTest
    {
        [Fact]
        public void TestEmptyCase()
        {
            var provider = new GenericFilterExpressionProvider<Entity, int>(
                i => new FilterExpressionDictionary<Entity>(),
                new FilterToIntegerConverter(new CultureInfoConversionPriorityCollection()));
            Assert.Null(provider.Resolve(FilterType.Equals, "1"));
        }

        [Fact]
        public void TestIncompatibleFilter()
        {
            var provider = new GenericFilterExpressionProvider<Entity, int>(
                i => new FilterExpressionDictionary<Entity>(),
                new FilterToIntegerConverter(new CultureInfoConversionPriorityCollection()));
            Assert.Null(provider.Resolve(FilterType.Equals, "a"));
        }

        [Fact]
        public void TestExistingCase()
        {
            const int id = 1;
            var provider = new GenericFilterExpressionProvider<Entity, int>(
                i => new FilterExpressionDictionary<Entity>().AddEquals(e => e.Id == i),
                new FilterToIntegerConverter(new CultureInfoConversionPriorityCollection()));
            var expression = provider.Resolve(FilterType.Equals, id.ToString());
            using (var context = new MockDbContext())
            {
                context.Add(new Entity { Id = id });
                context.SaveChanges();
                Assert.Equal(1, context.Entities.Count(expression));
            }
        }

        [Fact]
        public void TestExistingFilterWithIncompatibleValue()
        {
            var provider = new GenericFilterExpressionProvider<Entity, int>(
                i => new FilterExpressionDictionary<Entity>().AddEquals(e => e.Id == i),
                new FilterToIntegerConverter(new CultureInfoConversionPriorityCollection()));
            var expression = provider.Resolve(FilterType.Equals, "a");
            var constant = (ConstantExpression)expression.Body;
            Assert.False((bool)constant.Value);
        }
    }
}
