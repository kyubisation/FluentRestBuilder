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

    public class EntityDeletionPipe<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly IDbContextContainer dbContextContainer;

        public EntityDeletionPipe(
            IDbContextContainer dbContextContainer,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.dbContextContainer = dbContextContainer;
        }

        protected override async Task<IActionResult> GenerateActionResultAsync(TInput entity)
        {
            try
            {
                this.dbContextContainer.Context.Remove(entity);
                await this.dbContextContainer.Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return null;
        }
    }
}
