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
        public static IFluentRestBuilderCore RegisterFilterByClientRequestPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IFilterByClientRequestPipeFactory<>),
                typeof(FilterByClientRequestPipeFactory<>));
            builder.Services.TryAddScoped<
                IFilterByClientRequestInterpreter, FilterByClientRequestInterpreter>();
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
            builder.Services.TryAddTransient(typeof(FilterExpressionProviderDictionary<>));
            builder.Services.TryAddSingleton<IQueryArgumentKeys, QueryArgumentKeys>();
            return builder;
        }

        public static OutputPipe<IQueryable<TInput>> ApplyFilterByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                IDictionary<string, IFilterExpressionProvider<TInput>> filterExpressionProviders) =>
            pipe.GetService<IFilterByClientRequestPipeFactory<TInput>>()
                .Create(filterExpressionProviders, pipe);

        public static OutputPipe<IQueryable<TInput>> ApplyFilterByClientRequest<TInput>(
                this IOutputPipe<IQueryable<TInput>> pipe,
                Func<
                    FilterExpressionProviderDictionary<TInput>,
                    IDictionary<string, IFilterExpressionProvider<TInput>>> builder)
        {
            var providerDictionary = pipe.GetService<FilterExpressionProviderDictionary<TInput>>();
            var dictionary = builder(providerDictionary);
            return pipe.ApplyFilterByClientRequest(dictionary);
        }
    }
}
