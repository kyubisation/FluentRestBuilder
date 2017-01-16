// <copyright file="QueryableSourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.QueryableSource
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Pipes;
    using Microsoft.EntityFrameworkCore;

    public class QueryableSourcePipe<TInput, TOutput> : MappingPipeBase<TInput, IQueryable<TOutput>>
        where TOutput : class
    {
        private readonly Func<DbContext, TInput, IQueryable<TOutput>> queryablePipe;
        private readonly IDbContextContainer dbContextContainer;

        public QueryableSourcePipe(
            Func<DbContext, TInput, IQueryable<TOutput>> queryablePipe,
            IDbContextContainer dbContextContainer,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.queryablePipe = queryablePipe;
            this.dbContextContainer = dbContextContainer;
        }

        protected override IQueryable<TOutput> Map(TInput input) =>
            this.queryablePipe(this.dbContextContainer.Context, input);
    }
}
