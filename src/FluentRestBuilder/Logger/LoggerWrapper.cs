// <copyright file="LoggerWrapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Logger
{
    using System;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Internal;

    public class LoggerWrapper
    {
        private readonly ILogger logger;
        private readonly Lazy<LevelLogger> traceLogger;
        private readonly Lazy<LevelLogger> debugLogger;
        private readonly Lazy<LevelLogger> informationLogger;
        private readonly Lazy<LevelLogger> warningLogger;
        private readonly Lazy<LevelLogger> errorLogger;
        private readonly Lazy<LevelLogger> criticalLogger;

        public LoggerWrapper(ILogger logger)
        {
            if (logger == null)
            {
                return;
            }

            this.logger = logger;
            this.traceLogger = this.CreateLazyLevelLogger(LogLevel.Trace);
            this.debugLogger = this.CreateLazyLevelLogger(LogLevel.Debug);
            this.informationLogger = this.CreateLazyLevelLogger(LogLevel.Information);
            this.warningLogger = this.CreateLazyLevelLogger(LogLevel.Warning);
            this.errorLogger = this.CreateLazyLevelLogger(LogLevel.Error);
            this.criticalLogger = this.CreateLazyLevelLogger(LogLevel.Critical);
        }

        public LevelLogger Trace => this.traceLogger?.Value;

        public LevelLogger Debug => this.debugLogger?.Value;

        public LevelLogger Information => this.informationLogger?.Value;

        public LevelLogger Warning => this.warningLogger?.Value;

        public LevelLogger Error => this.errorLogger?.Value;

        public LevelLogger Critical => this.criticalLogger?.Value;

        private Lazy<LevelLogger> CreateLazyLevelLogger(LogLevel level) =>
            new Lazy<LevelLogger>(() => this.CreateLevelLoggerOrNullIfNotEnabled(level));

        private LevelLogger CreateLevelLoggerOrNullIfNotEnabled(LogLevel level) =>
            this.logger.IsEnabled(level) ? new LevelLogger(this.logger, level) : null;

        public class LevelLogger
        {
            private readonly ILogger logger;
            private readonly LogLevel logLevel;

            internal LevelLogger(ILogger logger, LogLevel logLevel)
            {
                this.logger = logger;
                this.logLevel = logLevel;
            }

            public void Log(EventId eventId, Exception exception, string message, params object[] args) =>
                this.logger.Log(
                    this.logLevel,
                    eventId,
                    (object)new FormattedLogValues(message, args),
                    exception,
                    MessageFormatter);

            public void Log(EventId eventId, string message, params object[] args) =>
                this.logger.Log(
                    this.logLevel,
                    eventId,
                    (object)new FormattedLogValues(message, args),
                    null,
                    MessageFormatter);

            public void Log(string message, params object[] args) =>
                this.logger.Log(
                    this.logLevel,
                    0,
                    (object)new FormattedLogValues(message, args),
                    null,
                    MessageFormatter);

            private static string MessageFormatter(object state, Exception error)
            {
                return state.ToString();
            }
        }
    }
}
