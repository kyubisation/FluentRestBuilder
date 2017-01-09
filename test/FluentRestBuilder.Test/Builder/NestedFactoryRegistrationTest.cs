// <copyright file="NestedFactoryRegistrationTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Builder
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Common.Mocks;
    using FluentRestBuilder.Sources.Source;
    using Microsoft.Extensions.DependencyInjection;
    using NestedFactoryRegistrationTestMocks;
    using Xunit;

    public class NestedFactoryRegistrationTest
    {
        [Fact]
        public void TestNestedReflection()
        {
            var nestedTypes = typeof(TestPipe<>)
                .GetTypeInfo()
                .GetNestedTypes();
            Assert.Equal(1, nestedTypes.Length);

            var nestedFactory = nestedTypes.First();
            Assert.Equal(typeof(TestPipe<>.Factory), nestedFactory);

            var nestedFactoryInterfaces = nestedFactory.GetInterfaces();
            Assert.Equal(1, nestedFactoryInterfaces.Length);

            var genericTypeDefinition = nestedFactoryInterfaces
                .First()
                .GetGenericTypeDefinition();
            Assert.Equal(typeof(IPipeFactory<>), genericTypeDefinition);
        }

        [Fact]
        public void TestScopedRegistration()
        {
            var services = new ServiceCollection()
                .TryAddPipeWithScopedNestedFactory(typeof(TestPipe<>));
            this.TestPipeFactoryRegistration(services, Assert.NotSame);
        }

        [Fact]
        public void TestSingletonRegistration()
        {
            var services = new ServiceCollection()
                .TryAddPipeWithSingletonNestedFactory(typeof(TestPipe<>));
            this.TestPipeFactoryRegistration(services, Assert.Same);
        }

        public void TestPipeFactoryRegistration(
            IServiceCollection services,
            Action<IPipeFactory<Entity>, IPipeFactory<Entity>> newScopeAssertion)
        {
            var provider = services.BuildServiceProvider();
            IPipeFactory<Entity> factory;
            using (var scope = provider.CreateScope())
            {
                factory = scope.ServiceProvider.GetService<IPipeFactory<Entity>>();
                Assert.NotNull(factory);
                var source = new Source<Entity>(new Entity(), scope.ServiceProvider);
                var pipe = factory.Resolve(source);
                Assert.IsType<TestPipe<Entity>>(pipe);
            }

            using (var scope = provider.CreateScope())
            {
                var scopedFactory = scope.ServiceProvider.GetService<IPipeFactory<Entity>>();
                newScopeAssertion(factory, scopedFactory);
            }
        }
    }
}
