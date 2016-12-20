// <copyright file="QueryablePipe.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Pipes.Queryable
{
    using System;
    using System.Linq;

    public class QueryablePipe<TInput> : BaseMappingPipe<IQueryable<TInput>, IQueryable<TInput>>
        where TInput : class
    {
        private readonly Func<IQueryable<TInput>, IQueryable<TInput>> callback;

        public QueryablePipe(
            Func<IQueryable<TInput>, IQueryable<TInput>> callback,
            IOutputPipe<IQueryable<TInput>> parent)
            : base(parent)
        {
            this.callback = callback;
        }

        protected override IQueryable<TInput> Map(IQueryable<TInput> input) => this.callback(input);
    }
}
