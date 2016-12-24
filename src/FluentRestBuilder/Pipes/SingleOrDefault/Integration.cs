﻿// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.SingleOrDefault;

    public static partial class Integration
    {
        public static OutputPipe<TInput> SingleOrDefault<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.GetService<ISingleOrDefaultPipeFactory<TInput>>().Resolve(predicate, pipe);
    }
}
