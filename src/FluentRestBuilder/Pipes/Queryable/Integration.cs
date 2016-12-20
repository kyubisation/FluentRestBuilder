// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Pipes.Queryable;

    public static partial class Integration
    {
        public static OutputPipe<IQueryable<TInput>> Where<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            new QueryablePipe<TInput>(q => q.Where(predicate), pipe);
    }
}
