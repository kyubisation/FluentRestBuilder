// <copyright file="EntityUpdatePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EntityUpdatePipe<TInput> : ActionResultPipe<TInput>
        where TInput : class
    {
        private readonly IDbContextContainer dbContextContainer;

        public EntityUpdatePipe(
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
