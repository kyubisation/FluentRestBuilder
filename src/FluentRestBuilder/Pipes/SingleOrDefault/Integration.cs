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
        public static IFluentRestBuilderCore RegisterSingleOrDefaultPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(ISingleOrDefaultPipeFactory<>), typeof(SingleOrDefaultPipeFactory<>));
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        /// <summary>
        /// Retrieves a single value from the provided <see cref="IQueryable{TInput}"/> or
        /// null, if empty.
        /// Throws an exception, if the result of the predicate contains more than one element.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="predicate">The predicate expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> SingleOrDefault<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class =>
            pipe.GetService<ISingleOrDefaultPipeFactory<TInput>>()
                .Create(predicate, pipe);
    }
}
