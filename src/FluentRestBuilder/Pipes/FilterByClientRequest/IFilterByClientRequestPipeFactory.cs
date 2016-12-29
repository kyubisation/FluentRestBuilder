// <copyright file="IFilterByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FilterByClientRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Expressions;

    public interface IFilterByClientRequestPipeFactory<TInput>
    {
        OutputPipe<IQueryable<TInput>> Resolve(
            IDictionary<string, IFilterExpressionProvider<TInput>> filterDictionary,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
