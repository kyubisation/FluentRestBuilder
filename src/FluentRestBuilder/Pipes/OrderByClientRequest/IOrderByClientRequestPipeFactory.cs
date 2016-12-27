// <copyright file="IOrderByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.OrderByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;

    public interface IOrderByClientRequestPipeFactory<TInput>
    {
        OutputPipe<IQueryable<TInput>> Resolve(
            IDictionary<string, IOrderByExpressionFactory<TInput>> orderByDictionary,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
