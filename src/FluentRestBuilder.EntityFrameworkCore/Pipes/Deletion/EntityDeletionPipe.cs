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
        private readonly IContextActions contextActions;

        public EntityDeletionPipe(
            IContextActions contextActions,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.contextActions = contextActions;
        }

        protected override async Task<IActionResult> GenerateActionResultAsync(TInput entity)
        {
            try
            {
                await this.contextActions.RemoveAndSave(entity);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            return null;
        }
    }
}
