// <copyright file="ExpressionFactoryTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Test.MetaModel
{
    using System.Linq;
    using System.Threading.Tasks;
    using EntityFrameworkCore.MetaModel;
    using FluentRestBuilder.Test.Common.Mocks;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ExpressionFactoryTest : ScopedDbContextTestBase
    {
        private readonly ExpressionFactory<MultiKeyEntity> multiKeyFactory;
        private readonly ExpressionFactory<Parent> parentFactory;
        private readonly ExpressionFactory<Child> childFactory;

        public ExpressionFactoryTest()
        {
            this.CreateEntities();
            this.CreateMultiKeyEntities();
            this.multiKeyFactory = this.ResolveScoped<ExpressionFactory<MultiKeyEntity>>();
            this.parentFactory = this.ResolveScoped<ExpressionFactory<Parent>>();
            this.childFactory = this.ResolveScoped<ExpressionFactory<Child>>();
        }

        [Fact]
        public async Task TestPrimaryKeyFilterCreation()
        {
            const int id = 2;
            var expression = this.Factory.CreatePrimaryKeyFilterExpression(new object[] { id });
            var result = await this.Context.Entities.SingleAsync(expression);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task TestCompositePrimaryKeyFilterCreation()
        {
            const int firstId = 2;
            const int secondId = 2;
            var expression = this.multiKeyFactory.CreatePrimaryKeyFilterExpression(
                new object[] { firstId, secondId });
            var result = await this.Context.MultiKeyEntities.SingleAsync(expression);
            Assert.Equal(firstId, result.FirstId);
            Assert.Equal(secondId, result.SecondId);
        }

        [Fact]
        public void TestProperties()
        {
            Assert.Equal(2, this.parentFactory.DeclaredProperties.Count());
            Assert.Equal(2, this.childFactory.DeclaredProperties.Count());
        }
    }
}
