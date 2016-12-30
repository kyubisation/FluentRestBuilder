// <copyright file="SingleOrDefaultPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SingleOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class SingleOrDefaultPipeFactory<TInput> : ISingleOrDefaultPipeFactory<TInput>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public SingleOrDefaultPipeFactory(IQueryableTransformer<TInput> queryableTransformer)
        {
            this.queryableTransformer = queryableTransformer;
        }

        public OutputPipe<TInput> Resolve(
            Expression<Func<TInput, bool>> predicate, IOutputPipe<IQueryable<TInput>> parent) =>
            new SingleOrDefaultPipe<TInput>(predicate, this.queryableTransformer, parent);
    }
}