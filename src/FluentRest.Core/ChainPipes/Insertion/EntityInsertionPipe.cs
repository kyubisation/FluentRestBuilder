// <copyright file="EntityInsertionPipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.ChainPipes.Insertion
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EntityInsertionPipe<TInput> : InputOutputPipe<TInput>, IItemProvider
        where TInput : class
    {
        private readonly DbContext context;
        private readonly IOutputPipe<TInput> parent;
        private TInput storedEntity;

        public EntityInsertionPipe(
            DbContext context,
            IOutputPipe<TInput> parent)
            : base(parent)
        {
            this.context = context;
            this.parent = parent;
        }

        object IItemProvider.GetItem(Type itemType) =>
            itemType == typeof(TInput) ? this.storedEntity : this.parent.GetItem(itemType);

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

            this.storedEntity = entity;
            return null;
        }
    }
}
