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
    using Pipes;
    using Pipes.FilterByClientRequest;
    using Pipes.FilterByClientRequest.Converters;
    using Pipes.FilterByClientRequest.Expressions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCoreConfiguration RegisterFilterByClientRequestPipe(
            this IFluentRestBuilderCoreConfiguration builder)
        {
            builder.Services.TryAddScoped(
                typeof(IFilterByClientRequestPipeFactory<>),
                typeof(FilterByClientRequestPipeFactory<>));
            builder.Services.TryAddSingleton<
                IReadOnlyDictionary<string, FilterType>, FilterTypeDictionary>();
            builder.Services.TryAddScoped<
                IFilterByClientRequestInterpreter, FilterByClientRequestInterpreter>();
            builder.Services.TryAddSingleton<
                ICultureInfoConversionPriorityCollection, CultureInfoConversionPriorityCollection>();
            builder.Services.TryAddSingleton(
                typeof(IFilterToTypeConverter<>), typeof(GenericFilterToTypeConverter<>));
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<short>, FilterToShortConverter>();
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<int>, FilterToIntegerConverter>();
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<long>, FilterToLongConverter>();
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<decimal>, FilterToDecimalConverter>();
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<float>, FilterToFloatConverter>();
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<double>, FilterToDoubleConverter>();
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<bool>, FilterToBooleanConverter>();
            builder.Services.TryAddSingleton<
                IFilterToTypeConverter<DateTime>, FilterToDateTimeConverter>();
            builder.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            return builder;
        }

        /// <summary>
        /// Configure the filter capabilities for this pipe chain.
        /// Provide a dictionary where the keys are filterable fields/properties
        /// and the values are implementations of <see cref="IFilterExpressionProvider{TInput}"/>
        /// which provide the filter logic.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="filterExpressionProviders">The filter dictionary.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> ApplyFilterByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                IDictionary<string, IFilterExpressionProvider<TInput>> filterExpressionProviders)
        {
            var factory = pipe.GetService<IFilterByClientRequestPipeFactory<TInput>>();
            Check.IsPipeRegistered(factory, typeof(FilterByClientRequestPipe<>));
            return factory.Create(filterExpressionProviders, pipe);
        }

        /// <summary>
        /// Configure the filter capabilities for this pipe chain.
        /// Use the <see cref="FilterExpressionProviderDictionary{TInput}"/> to configure
        /// the available filters.
        /// StringComparer.OrdinalIgnoreCase is used for key comparison.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="factory">The configuration factory.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<IQueryable<TInput>> ApplyFilterByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<
                    FilterExpressionProviderDictionary<TInput>,
                    IDictionary<string, IFilterExpressionProvider<TInput>>> factory)
        {
            var providerDictionary = new FilterExpressionProviderDictionary<TInput>(pipe);
            var dictionary = factory(providerDictionary);
            return pipe.ApplyFilterByClientRequest(dictionary);
        }
    }
}
