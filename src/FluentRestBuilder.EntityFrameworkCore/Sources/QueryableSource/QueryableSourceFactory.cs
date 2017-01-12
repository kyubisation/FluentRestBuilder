// <copyright file="QueryableSourceFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class QueryableSourceFactory<TOutput> : IQueryableSourceFactory<TOutput>
        where TOutput : class
    {
        private readonly IQueryableFactory<TOutput> queryableFactory;
        private readonly IServiceProvider serviceProvider;

        public QueryableSourceFactory(
            IQueryableFactory<TOutput> queryableFactory,
            IServiceProvider serviceProvider)
        {
            this.queryableFactory = queryableFactory;
            this.serviceProvider = serviceProvider;
        }

        public OutputPipe<TOutput> Resolve(ControllerBase controller) =>
            new QueryableSource<TOutput>(this.queryableFactory, this.serviceProvider)
            { 
                Controller = controller
            };
    }
}