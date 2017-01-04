// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;
    using Pipes.SingleOrDefault;

    public static partial class Integration
    {
        internal static IFluentRestBuilder RegisterSingleOrDefaultPipe(
            this IFluentRestBuilder builder)
        {
            builder.Services.TryAddScoped(
                typeof(ISingleOrDefaultPipeFactory<>), typeof(SingleOrDefaultPipeFactory<>));
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        public static OutputPipe<TInput> SingleOrDefault<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.GetService<ISingleOrDefaultPipeFactory<TInput>>().Resolve(predicate, pipe);
    }
}
