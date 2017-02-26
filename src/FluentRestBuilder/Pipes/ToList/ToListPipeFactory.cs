// <copyright file="ToListPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ToList
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class ToListPipeFactory<TInput> : IToListPipeFactory<TInput>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;
        private readonly ILogger<ToListPipe<TInput>> logger;

        public ToListPipeFactory(
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<ToListPipe<TInput>> logger = null)
        {
            this.queryableTransformer = queryableTransformer;
            this.logger = logger;
        }

        public OutputPipe<List<TInput>> Create(IOutputPipe<IQueryable<TInput>> parent) =>
            new ToListPipe<TInput>(this.queryableTransformer, this.logger, parent);
    }
}