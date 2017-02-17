// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.OrderByClientRequest;
    using Pipes.OrderByClientRequest.Expressions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterOrderByClientRequestPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IOrderByClientRequestPipeFactory<>),
                typeof(OrderByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IOrderByClientRequestInterpreter, OrderByClientRequestInterpreter>();
            return builder;
        }

        /// <summary>
        /// Configure the order by capabilities for this pipe chain.
        /// Provide a dictionary where the keys are filterable fields/properties
        /// and the values are implementations of <see cref="IOrderByExpressionFactory{TInput}"/>
        /// which provide the order by logic.
        /// 
        /// To define the default order by, use the order by extension methods before this pipe
        /// (<see cref="OrderBy{TInput,TKey}"/>, <see cref="OrderByDescending{TInput,TKey}"/>,
        /// <see cref="ThenBy{TInput,TKey}"/> and/or <see cref="ThenByDescending{TInput,TKey}"/>).
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="orderByExpressions">The order by dictionary.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> ApplyOrderByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                IDictionary<string, IOrderByExpressionFactory<TInput>> orderByExpressions) =>
            pipe.GetService<IOrderByClientRequestPipeFactory<TInput>>()
                .Create(orderByExpressions, pipe);

        /// <summary>
        /// Configure the order by capabilities for this pipe chain.
        /// Use the <see cref="OrderByExpressionDictionary{TInput}"/> to configure
        /// the available order by possibilities.
        /// 
        /// To define the default order by, use the order by extension methods before this pipe
        /// (<see cref="OrderBy{TInput,TKey}"/>, <see cref="OrderByDescending{TInput,TKey}"/>,
        /// <see cref="ThenBy{TInput,TKey}"/> and/or <see cref="ThenByDescending{TInput,TKey}"/>).
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="factory">The configuration factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> ApplyOrderByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<
                    OrderByExpressionDictionary<TInput>,
                    IDictionary<string, IOrderByExpressionFactory<TInput>>> factory)
        {
            var dictionary = factory(new OrderByExpressionDictionary<TInput>());
            return pipe.ApplyOrderByClientRequest(dictionary);
        }

        /// <summary>
        /// Configure the order by capabilities for this pipe chain.
        /// Use the <see cref="OrderByExpressionDictionary{TInput}"/> to configure
        /// the available order by possibilities.
        /// This uses the <see cref="StringComparer.InvariantCultureIgnoreCase"/> for
        /// key comparison.
        /// 
        /// To define the default order by, use the order by extension methods before this pipe
        /// (<see cref="OrderBy{TInput,TKey}"/>, <see cref="OrderByDescending{TInput,TKey}"/>,
        /// <see cref="ThenBy{TInput,TKey}"/> and/or <see cref="ThenByDescending{TInput,TKey}"/>).
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="factory">The configuration factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> ApplyCaseInsensitiveOrderByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<
                    OrderByExpressionDictionary<TInput>,
                    IDictionary<string, IOrderByExpressionFactory<TInput>>> factory)
        {
            var dictionary = factory(
                new OrderByExpressionDictionary<TInput>(
                    StringComparer.InvariantCultureIgnoreCase));
            return pipe.ApplyOrderByClientRequest(dictionary);
        }
    }
}
