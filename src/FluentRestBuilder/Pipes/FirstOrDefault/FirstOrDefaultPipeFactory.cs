// <copyright file="FirstOrDefaultPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FirstOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Common;

    public class FirstOrDefaultPipeFactory<TInput> : IFirstOrDefaultPipeFactory<TInput>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public FirstOrDefaultPipeFactory(IQueryableTransformer<TInput> queryableTransformer)
        {
            this.queryableTransformer = queryableTransformer;
        }

        public OutputPipe<TInput> Resolve(
            Expression<Func<TInput, bool>> predicate, IOutputPipe<IQueryable<TInput>> parent) =>
            new FirstOrDefaultPipe<TInput>(predicate, this.queryableTransformer, parent);
    }
}