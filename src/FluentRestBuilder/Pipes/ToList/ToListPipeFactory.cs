// <copyright file="ToListPipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.ToList
{
    using System.Collections.Generic;
    using System.Linq;

    public class ToListPipeFactory<TInput> : IToListPipeFactory<TInput>
    {
        private readonly IQueryableTransformer<TInput> queryableTransformer;

        public ToListPipeFactory(
            IQueryableTransformer<TInput> queryableTransformer)
        {
            this.queryableTransformer = queryableTransformer;
        }

        public OutputPipe<List<TInput>> Create(IOutputPipe<IQueryable<TInput>> parent) =>
            new ToListPipe<TInput>(this.queryableTransformer, parent);
    }
}