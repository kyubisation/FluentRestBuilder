// <copyright file="ToListPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ToList
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ToListPipe<TInput> : MappingPipeBase<IQueryable<TInput>, List<TInput>>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public ToListPipe(
            IQueryableTransformer<TInput> queryableTransformer,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.queryableTransformer = queryableTransformer;
        }

        protected override Task<List<TInput>> MapAsync(IQueryable<TInput> input) =>
            this.queryableTransformer.ToList(input);
    }
}
