// <copyright file="LevelLogger.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Logger
{
    using System;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Internal;

    public class LevelLogger
    {
        private readonly ILogger logger;
        private readonly LogLevel logLevel;

        public LevelLogger(ILogger logger, LogLevel logLevel)
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
