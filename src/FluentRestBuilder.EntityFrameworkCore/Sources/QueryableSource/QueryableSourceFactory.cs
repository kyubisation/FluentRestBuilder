// <copyright file="QueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Storage;

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

        public OutputPipe<IQueryable<TOutput>> Resolve(ControllerBase controller) =>
            new QueryableSource<TOutput>(this.contextStorage, this.serviceProvider)
            {
                Controller = controller
            };
    }
}