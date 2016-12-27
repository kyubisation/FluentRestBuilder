// <copyright file="QueryableSource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FluentRestBuilder.Sources;

    public class QueryableSource<TEntity> : BaseSource<IQueryable<TEntity>>
        where TEntity : class
    {
        private readonly IQueryableFactory<TEntity> queryableFactory;

        public QueryableSource(
            IQueryableFactory<TEntity> queryableFactory,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.queryableFactory = queryableFactory;
        }

        protected override Task<IQueryable<TEntity>> GetOutput() =>
            Task.FromResult(this.queryableFactory.Queryable);
    }
}
