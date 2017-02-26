// <copyright file="ToListPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ToList
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class ToListPipe<TInput> : MappingPipeBase<IQueryable<TInput>, List<TInput>>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public ToListPipe(
            IQueryableTransformer<TInput> queryableTransformer,
            ILogger<ToListPipe<TInput>> logger,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(logger, parent)
        {
            this.queryableTransformer = queryableTransformer;
        }

        protected override Task<List<TInput>> MapAsync(IQueryable<TInput> input) =>
            this.queryableTransformer.ToList(input);
    }
}
