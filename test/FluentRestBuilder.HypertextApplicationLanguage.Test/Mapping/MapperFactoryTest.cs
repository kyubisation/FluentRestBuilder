// <copyright file="MapperFactoryTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Test.Mapping
{
    using System;
    using FluentRestBuilder.Builder;
    using FluentRestBuilder.Mocks.EntityFramework;
    using HypertextApplicationLanguage.Mapping;
    using Microsoft.Extensions.DependencyInjection;
    using Mocks;
    using Xunit;

    public class MapperFactoryTest
    {
        private readonly IServiceProvider provider;

        public MapperFactoryTest()
        {
            this.provider = new FluentRestBuilderConfiguration(new ServiceCollection())
                .AddHAL()
                .Services
                .BuildServiceProvider();
        }

        [Fact]
        public void TestResolvingGenericMapperFactory()
        {
            var factory = new MapperFactory(this.provider);
            var genericFactory = factory.Resolve<Entity>();
            Assert.NotNull(genericFactory);
        }

        [Fact]
        public void TestResolvingDefaultReflectionMapper()
        {
            var factory = new MapperFactory<Entity>(this.provider);
            var mapper = factory.Resolve<EntityResponse>();
            Assert.NotNull(mapper);
        }
    }
}
