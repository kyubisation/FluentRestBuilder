// <copyright file="EntityCollectionSourcePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Pipes.EntityCollectionSource
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Core.Common;
    using Core.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Sources.EntityCollection;

    public class EntityCollectionSourcePipe<TInput, TOutput> :
        EntityCollectionSource<TOutput>,
        IInputPipe<TInput>
        where TOutput : class
    {
        private readonly IOutputPipe<TInput> parent;
        private readonly Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe;

        public EntityCollectionSourcePipe(
            Func<IQueryable<TOutput>, TInput, IQueryable<TOutput>> queryablePipe,
            IQueryable<TOutput> queryable,
            IScopedStorage<PaginationMetaInfo> paginationMetaInfoStorage,
            IOutputPipe<TInput> parent)
            : base(queryable, paginationMetaInfoStorage, parent)
        {
            this.queryablePipe = queryablePipe;
            this.parent = parent;
            this.parent.Attach(this);
        }

        Task<IActionResult> IPipe.Execute() => this.parent.Execute();

        Task<IActionResult> IInputPipe<TInput>.Execute(TInput input)
        {
            if (this.queryablePipe != null)
            {
                this.Queryable = this.queryablePipe(this.Queryable, input);
            }

            return this.ExecuteAsync();
        }
    }
}
