// <copyright file="FirstOrDefaultPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.FirstOrDefault
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Extensions.Logging;

    public class FirstOrDefaultPipeFactory<TInput> : IFirstOrDefaultPipeFactory<TInput>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;
        private readonly ILogger<FirstOrDefaultPipe<TInput>> logger;

        public FirstOrDefaultPipeFactory(
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<FirstOrDefaultPipe<TInput>> logger = null)
        {
            this.queryableTransformer = queryableTransformer;
            this.logger = logger;
        }

        public OutputPipe<TInput> Create(
            Expression<Func<TInput, bool>> predicate, IOutputPipe<IQueryable<TInput>> parent) =>
            new FirstOrDefaultPipe<TInput>(
                predicate, this.queryableTransformer, this.logger, parent);
    }
}