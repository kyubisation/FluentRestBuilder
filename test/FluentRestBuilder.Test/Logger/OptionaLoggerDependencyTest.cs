// <copyright file="OptionaLoggerDependencyTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Logger
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Xunit;

    public class OptionaLoggerDependencyTest
    {
        [Fact]
        public void TestUnavailableDependency()
        {
            var provider = new ServiceCollection()
                .AddTransient<OptionalArgumentClass>()
                .BuildServiceProvider();
            var instance = provider.GetService<OptionalArgumentClass>();
            Assert.NotNull(instance);
            Assert.Null(instance.Logger);
        }

        [Fact]
        public void TestAvailableDependency()
        {
            var provider = new ServiceCollection()
                .AddSingleton<ILogger<object>, EmptyLogger<object>>()
                .AddTransient<OptionalArgumentClass>()
                .BuildServiceProvider();
            var instance = provider.GetService<OptionalArgumentClass>();
            Assert.NotNull(instance);
            Assert.NotNull(instance.Logger);
        }

        public class OptionalArgumentClass
        {
            public OptionalArgumentClass(ILogger<object> logger = null)
            {
                this.Logger = logger;
            }

            public ILogger<object> Logger { get; set; }
        }

        public class EmptyLogger<T> : ILogger<T>
        {
            public void Log<TState>(
                LogLevel logLevel,
                EventId eventId,
                TState state,
                Exception exception,
                Func<TState, Exception, string> formatter)
            {
                throw new NotImplementedException();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                throw new NotImplementedException();
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }
        }
    }
}
