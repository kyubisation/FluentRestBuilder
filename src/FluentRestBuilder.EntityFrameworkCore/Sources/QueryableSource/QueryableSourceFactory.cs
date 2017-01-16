// <copyright file="QueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    public class QueryableSourceFactory<TOutput> : IQueryableSourceFactory<TOutput>
        where TOutput : class
    {
        private readonly IDbContextContainer dbContextContainer;
        private readonly IServiceProvider serviceProvider;

        public QueryableSourceFactory(
            IDbContextContainer dbContextContainer,
            IServiceProvider serviceProvider)
        {
            this.dbContextContainer = dbContextContainer;
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<IQueryable<TOutput>> Resolve(ControllerBase controller) =>
            new QueryableSource<TOutput>(this.dbContextContainer, this.serviceProvider)
            {
                Controller = controller
            };
    }
}