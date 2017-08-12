// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Linq;
    using Builder;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using HypertextApplicationLanguage.Pipes.CollectionMapping;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterCollectionMappingPipe(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddSingleton<ILinkAggregator, LinkAggregator>();
            builder.Services.TryAddScoped(
                typeof(ICollectionMappingPipeFactory<,>), typeof(CollectionMappingPipeFactory<,>));
            builder.Services
                .TryAddScoped<IRestCollectionLinkGenerator, RestCollectionLinkGenerator>();
            builder.Services.TryAddSingleton(
                typeof(IQueryableTransformer<>), typeof(QueryableTransformer<>));
            return builder;
        }

        /// <summary>
        /// Maps the entries of the received <see cref="IQueryable{TInput}"/>
        /// according to the given mapping function and wraps the result
        /// in an <see cref="RestEntityCollection"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<RestEntityCollection> MapToRestCollection<TInput, TOutput>(
            this IOutputPipe<IQueryable<TInput>> pipe, Func<TInput, TOutput> mapping)
            where TInput : class
            where TOutput : class
        {
            var factory = pipe.GetService<ICollectionMappingPipeFactory<TInput, TOutput>>();
            Check.IsPipeRegistered(factory, typeof(CollectionMappingPipe<,>));
            return factory.Create(mapping, pipe);
        }
    }
}
