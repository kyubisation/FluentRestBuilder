// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System.Collections.Generic;
    using System.Linq;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;
    using Pipes.ToList;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterToListPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IToListPipeFactory<>), typeof(ToListPipeFactory<>));
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        /// <summary>
        /// Converts the received <see cref="IQueryable{TInput}"/> to a <see cref="List{TInput}"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<List<TInput>> ToList<TInput>(
            this IOutputPipe<IQueryable<TInput>> pipe)
            where TInput : class
        {
            var factory = pipe.GetService<IToListPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(ToListPipe<>));
            return factory.Create(pipe);
        }
    }
}
