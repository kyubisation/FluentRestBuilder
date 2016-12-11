// <copyright file="EntityUpdatePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.Update
{
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Pipes.Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Storage;

    public class EntityUpdatePipe<TInput> : InputOutputPipe<TInput>
        where TInput : class
    {
        private readonly DbContext context;
        private readonly IScopedStorage<TInput> storage;

        public EntityUpdatePipe(
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
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            var queryable = this.GetService<IQueryable<TInput>>();
            if (queryable != null)
            {
                this.storage.Value = await queryable.SingleAsync();
            }

            return null;
        }
    }
}
