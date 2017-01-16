// <copyright file="QueryableSource.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Sources.QueryableSource
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentRestBuilder.Sources;

    public class QueryableSource<TEntity> : SourceBase<IQueryable<TEntity>>
        where TEntity : class
    {
        private readonly IDbContextContainer dbContextContainer;

        public QueryableSource(
            IDbContextContainer dbContextContainer,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.dbContextContainer = dbContextContainer;
        }

        protected override Task<IQueryable<TEntity>> GetOutput()
        {
            IQueryable<TEntity> queryable = this.dbContextContainer.Context.Set<TEntity>();
            return Task.FromResult(queryable);
        }
    }
}
