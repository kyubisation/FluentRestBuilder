// <copyright file="EntityDeletionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class EntityDeletionPipe<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntityDeletionPipe(
            IScopedStorage<DbContext> contextStorage,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.contextStorage = contextStorage;
        }

        protected override async Task<IActionResult> GenerateActionResultAsync(TInput entity)
        {
            try
            {
                this.contextStorage.Value.Remove(entity);
                await this.contextStorage.Value.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return null;
        }
    }
}
