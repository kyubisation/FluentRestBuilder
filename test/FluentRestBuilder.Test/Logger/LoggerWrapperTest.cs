// <copyright file="LoggerWrapperTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Logger
{
    using System;
    using FluentRestBuilder.Logger;
    using Microsoft.Extensions.Logging;
    using Xunit;

    public class LoggerWrapperTest
    {
        [Fact]
        public void TestNullLogger()
        {
            var logger = new LoggerWrapper(null);
            Assert.Null(logger.Critical);
            Assert.Null(logger.Debug);
            Assert.Null(logger.Error);
            Assert.Null(logger.Information);
            Assert.Null(logger.Trace);
            Assert.Null(logger.Warning);
        }

        [Fact]
        public void TestDisabledTraceLogger()
        {
            var loggerFactory = new LoggerFactory();
            var originalLogger = loggerFactory.CreateLogger<object>();
            Assert.False(originalLogger.IsEnabled(LogLevel.Trace));
            var logger = new LoggerWrapper(originalLogger);
            Assert.Null(logger.Trace);
        }

        [Fact]
        public void TestParametersForDisabledTraceLogger()
        {
            var loggerFactory = new LoggerFactory();
            var logger = new LoggerWrapper(loggerFactory.CreateLogger<object>());
            var lazy = new Lazy<int>(() => 1);
            logger.Trace?.Log($"Test {lazy.Value}");
            Assert.False(lazy.IsValueCreated);
        }

        [Fact]
        public void TestEnabledTraceLogger()
        {
            var emptyLogger = new Logger();
            Assert.True(emptyLogger.IsEnabled(LogLevel.Trace));
            var logger = new LoggerWrapper(emptyLogger);
            Assert.NotNull(logger.Critical);
            Assert.NotNull(logger.Debug);
            Assert.NotNull(logger.Error);
            Assert.NotNull(logger.Information);
            Assert.NotNull(logger.Trace);
            Assert.NotNull(logger.Warning);
        }

        private class Logger : ILogger
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

            public bool IsEnabled(LogLevel logLevel) => true;

            public IDisposable BeginScope<TState>(TState state)
            {
                throw new NotImplementedException();
            }
        }
    }
}
