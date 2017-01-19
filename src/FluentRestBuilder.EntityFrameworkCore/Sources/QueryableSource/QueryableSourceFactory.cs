// <copyright file="QueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using System.Linq;
    using FluentRestBuilder.Storage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class QueryableSourceFactory<TOutput> : IQueryableSourceFactory<TOutput>
        where TOutput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;
        private readonly IServiceProvider serviceProvider;

        public QueryableSourceFactory(
            IScopedStorage<DbContext> contextStorage,
            IServiceProvider serviceProvider)
        {
            this.contextStorage = contextStorage;
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<IQueryable<TOutput>> Create(ControllerBase controller) =>
            new QueryableSource<TOutput>(this.contextStorage, this.serviceProvider)
            {
                Controller = controller
            };
    }
}