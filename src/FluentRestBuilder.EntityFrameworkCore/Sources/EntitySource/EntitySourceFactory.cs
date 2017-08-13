// <copyright file="EntitySourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.EntitySource
{
    using System;
    using System.Linq.Expressions;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class EntitySourceFactory<TOutput> : IEntitySourceFactory<TOutput>
        where TOutput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;
        private readonly ILogger<EntitySource<TOutput>> logger;

        public EntitySourceFactory(
            IScopedStorage<DbContext> contextStorage,
            ILogger<EntitySource<TOutput>> logger = null)
        {
            this.contextStorage = contextStorage;
            this.logger = logger;
        }

        public OutputPipe<TOutput> Create(
            ControllerBase controller, Expression<Func<TOutput, bool>> predicate) =>
            new EntitySource<TOutput>(
                predicate,
                this.contextStorage,
                this.logger,
                controller.HttpContext.RequestServices);
    }
}