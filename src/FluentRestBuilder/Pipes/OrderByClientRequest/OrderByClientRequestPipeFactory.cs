// <copyright file="OrderByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;

    public class OrderByClientRequestPipeFactory<TInput> : IOrderByClientRequestPipeFactory<TInput>
    {
        private readonly IOrderByClientRequestInterpreter interpreter;

        public OrderByClientRequestPipeFactory(IOrderByClientRequestInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public OutputPipe<IQueryable<TInput>> Create(
            IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary,
            IOutputPipe<IQueryable<TInput>> parent) =>
            new OrderByClientRequestPipe<TInput>(
                orderByDictionary, this.interpreter, parent);
    }
}