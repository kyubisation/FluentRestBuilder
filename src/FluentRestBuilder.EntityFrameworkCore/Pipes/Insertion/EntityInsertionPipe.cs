// <copyright file="EntityInsertionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class EntityInsertionPipe<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntityInsertionPipe(
            IScopedStorage<DbContext> contextStorage,
            ILogger<EntityInsertionPipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.contextStorage = contextStorage;
        }

        protected override async Task<IActionResult> GenerateActionResult(TInput entity)
        {
            try
            {
                this.contextStorage.Value.Add(entity);
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
