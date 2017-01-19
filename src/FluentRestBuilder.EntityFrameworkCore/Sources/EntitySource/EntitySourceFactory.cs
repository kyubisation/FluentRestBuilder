// <copyright file="EntitySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.EntitySource
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Storage;

    public class EntitySourceFactory<TOutput> : IEntitySourceFactory<TOutput>
        where TOutput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;

        public EntitySourceFactory(IScopedStorage<DbContext> contextStorage)
        {
            this.contextStorage = contextStorage;
        }

        public OutputPipe<TOutput> Create(
            ControllerBase controller, Expression<Func<TOutput, bool>> predicate) =>
            new EntitySource<TOutput>(
                predicate, this.contextStorage, controller.HttpContext.RequestServices)
            {
                Controller = controller
            };
    }
}