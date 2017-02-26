// <copyright file="OrderByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;
    using Microsoft.Extensions.Logging;

    public class OrderByClientRequestPipeFactory<TInput> : IOrderByClientRequestPipeFactory<TInput>
    {
        private readonly IOrderByClientRequestInterpreter interpreter;
        private readonly ILogger<OrderByClientRequestPipe<TInput>> logger;

        public OrderByClientRequestPipeFactory(
            IOrderByClientRequestInterpreter interpreter,
            ILogger<OrderByClientRequestPipe<TInput>> logger = null)
        {
            this.interpreter = interpreter;
            this.logger = logger;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new OrderByClientRequestPipe<TInput>(
                orderByDictionary, this.interpreter, this.logger, parent);
    }
}