// <copyright file="ISearchByClientRequestPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SearchByClientRequest
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ISearchByClientRequestPipeFactory<TInput>
    {
        OutputPipe<IQueryable<TInput>> Resolve(
            Func<string, Expression<Func<TInput, bool>>> search,
            IOutputPipe<IQueryable<TInput>> parent);
    }
}
