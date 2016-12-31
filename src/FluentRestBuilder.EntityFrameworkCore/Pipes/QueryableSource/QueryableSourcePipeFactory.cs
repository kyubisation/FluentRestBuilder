// <copyright file="QueryableSourcePipeFactory.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.EntityFrameworkCore.Pipes.QueryableSource
{
    using System;
    using System.Linq;
    using QueryableFactories;

    public class QueryableSourcePipeFactory<TInput, TOutput> :
        IQueryableSourcePipeFactory<TInput, TOutput>
        where TOutput : class
    {
        private readonly IQueryableFactory queryableFactory;

        public QueryableSourcePipeFactory(IQueryableFactory queryableFactory)
        {
            this.queryableFactory = queryableFactory;
        }

        public QueryableSourcePipe<TInput, TOutput> Resolve(
            Func<IQueryableFactory, TInput, IQueryable<TOutput>> selection,
            IOutputPipe<TInput> pipe) =>
            new QueryableSourcePipe<TInput, TOutput>(selection, this.queryableFactory, pipe);
    }
}