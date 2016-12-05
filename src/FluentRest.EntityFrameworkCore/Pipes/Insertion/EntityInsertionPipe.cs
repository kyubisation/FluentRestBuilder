// <copyright file="EntityInsertionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.EntityFrameworkCore.Pipes.Insertion
{
    using System.Threading.Tasks;
    using Core;
    using Core.Pipes.Common;
    using Core.Storage;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EntityInsertionPipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly DbContext context;
        private readonly IScopedStorage<TInput> storage;

        public EntityInsertionPipe(
            DbContext context,
            IScopedStorage<TInput> storage,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.context = context;
            this.storage = storage;
        }

        protected override async Task<IActionResult> ExecuteAsync(TInput entity)
        {
            this.context.Set<TInput>().Add(entity);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            this.storage.Value = entity;
            return null;
        }
    }
}
