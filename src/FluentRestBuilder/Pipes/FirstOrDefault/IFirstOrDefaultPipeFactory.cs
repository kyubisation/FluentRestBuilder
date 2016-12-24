// <copyright file="IFirstOrDefaultPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FirstOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IFirstOrDefaultPipeFactory<TInput>
    {
        OutputPipe<TInput> Resolve(
            Expression<Func<TInput, bool>> predicate, IOutputPipe<IQueryable<TInput>> parent);
    }
}
