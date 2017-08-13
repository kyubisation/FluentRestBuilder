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
    using Microsoft.Extensions.Logging;

    public class QueryableSourceFactory<TOutput> : IQueryableSourceFactory<TOutput>
        where TOutput : class
    {
        private readonly IScopedStorage<DbContext> contextStorage;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<QueryableSource<TOutput>> logger;

        public QueryableSourceFactory(
            IScopedStorage<DbContext> contextStorage,
            IServiceProvider serviceProvider,
            ILogger<QueryableSource<TOutput>> logger = null)
        {
            this.contextStorage = contextStorage;
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public OutputPipe<IQueryable<TOutput>> Create(ControllerBase controller) =>
            new QueryableSource<TOutput>(this.contextStorage, this.logger, this.serviceProvider);
    }
}