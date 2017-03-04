// <copyright file="EntityUpdatePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class EntityUpdatePipe<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntityUpdatePipe(
            IScopedStorage<DbContext> contextStorage,
            ILogger<EntityUpdatePipe<TInput>> logger,
            IOutputPipe<TInput> parent)
            : base(logger, parent)
        {
            this.contextStorage = contextStorage;
        }

        protected override async Task<IActionResult> GenerateActionResult(TInput entity)
        {
            try
            {
                this.Logger.Information?.Log(
                    "Attempting to update an instance of {0} in the database", typeof(TInput));
                await this.contextStorage.Value.SaveChangesAsync();
                this.Logger.Debug?.Log("Insertion of {0} was successful", typeof(TInput));
            }
            catch (DbUpdateConcurrencyException exception)
            {
                this.Logger.Information?.Log(
                    0,
                    exception,
                    "Failed to update the received value due to a conflict",
                    typeof(TInput));
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return null;
        }
    }
}
