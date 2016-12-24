// <copyright file="ISingleOrDefaultPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SingleOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface ISingleOrDefaultPipeFactory<TInput>
    {
        OutputPipe<TInput> Resolve(
            Expression<Func<TInput, bool>> predicate, IOutputPipe<IQueryable<TInput>> parent);
    }
}
