// <copyright file="SingleOrDefaultPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.SingleOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Extensions.Logging;

    public class SingleOrDefaultPipeFactory<TInput> : ISingleOrDefaultPipeFactory<TInput>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;
        private readonly ILogger<SingleOrDefaultPipe<TInput>> logger;

        public SingleOrDefaultPipeFactory(
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<SingleOrDefaultPipe<TInput>> logger = null)
        {
            this.queryableTransformer = queryableTransformer;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(
            Expression<Func<TInput, bool>> predicate, IOutputPipe<IQueryable<TInput>> parent) =>
            new SingleOrDefaultPipe<TInput>(
                predicate, this.queryableTransformer, this.logger, parent);
    }
}