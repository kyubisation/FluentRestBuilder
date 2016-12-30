// <copyright file="QueryableSourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.QueryableSource
{
    using System;
    using System.Linq;
    using Common;
    using FluentRestBuilder.Pipes;

    public class QueryableSourcePipe<TInput, TOutput> : MappingPipeBase<TInput, IQueryable<TOutput>>
        where TOutput : class
    {
        private readonly Func<IQueryableFactory, TInput, IQueryable<TOutput>> queryablePipe;
        private readonly IQueryableFactory queryableFactory;

        public QueryableSourcePipe(
            Func<IQueryableFactory, TInput, IQueryable<TOutput>> queryablePipe,
            IQueryableFactory queryableFactory,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.queryablePipe = queryablePipe;
            this.queryableFactory = queryableFactory;
        }

        protected override IQueryable<TOutput> Map(TInput input) =>
            this.queryablePipe(this.queryableFactory, input);
    }
}
