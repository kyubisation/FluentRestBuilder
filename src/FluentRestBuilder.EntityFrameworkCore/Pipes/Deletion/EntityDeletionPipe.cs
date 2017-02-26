// <copyright file="EntityDeletionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Deletion
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class EntityDeletionPipe<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntityDeletionPipe(
            IScopedStorage<DbContext> contextStorage,
            ILogger<EntityDeletionPipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.contextStorage = contextStorage;
        }

        protected override async Task<IActionResult> GenerateActionResult(TInput entity)
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
