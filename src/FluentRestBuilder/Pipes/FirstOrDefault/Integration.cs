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
    using Pipes.FirstOrDefault;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterFirstOrDefaultPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IFirstOrDefaultPipeFactory<>), typeof(FirstOrDefaultPipeFactory<>));
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        /// <summary>
        /// Retrieves the first value from the provided <see cref="IQueryable{TInput}"/> or
        /// null, if empty.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="predicate">The predicate expression.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TInput> FirstOrDefault<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Expression<Func<TInput, bool>> predicate)
            where TInput : class
        {
            var factory = pipe.GetService<IFirstOrDefaultPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(FirstOrDefaultPipe<>));
            return factory.Create(predicate, pipe);
        }
    }
}
