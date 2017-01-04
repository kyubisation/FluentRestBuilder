// <copyright file="EntityInsertionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Insertion
{
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class EntityInsertionPipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly IContextActions contextActions;
        private readonly IScopedStorage<TInput> storage;

        public EntityInsertionPipe(
            IContextActions contextActions,
            IScopedStorage<TInput> storage,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.contextActions = contextActions;
            this.storage = storage;
        }

        protected override async Task<IActionResult> ExecuteAsync(TInput entity)
        {
            try
            {
                await this.contextActions.AddAndSave(entity);
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
