// <copyright file="FluentRestBuilderConfiguration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using System;
    using Json;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Operators.ClientRequest.FilterConverters;
    using Operators.ClientRequest.Interpreters;
    using Storage;

    public class FluentRestBuilderConfiguration : IFluentRestBuilderConfiguration
    {
        public FluentRestBuilderConfiguration(IServiceCollection services)
        {
            this.Services = services;
            this.Services.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
            this.Services.TryAddScoped<IJsonPropertyNameResolver, JsonPropertyNameResolver>();
            this.RegisterInterpreters();
            this.RegisterFilterConverters();
        }

        public IServiceCollection Services { get; }

        private void RegisterInterpreters()
        {
            this.Services.TryAddScoped<
                IFilterByClientRequestInterpreter, FilterByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                IOrderByClientRequestInterpreter, OrderByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                IPaginationByClientRequestInterpreter, PaginationByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                ISearchByClientRequestInterpreter, SearchByClientRequestInterpreter>();
        }

        private void RegisterFilterConverters()
        {
            this.Services.TryAddSingleton<
                ICultureInfoConversionPriorityCollection, CultureInfoConversionPriorityCollection>();
            this.Services.TryAddSingleton(
                typeof(IFilterToTypeConverter<>), typeof(GenericFilterToTypeConverter<>));
            this.Services.TryAddSingleton<IFilterToTypeConverter<bool>, FilterToBooleanConverter>();
            this.Services.TryAddSingleton<IFilterToTypeConverter<DateTime>, FilterToDateTimeConverter>();
            this.Services.TryAddSingleton<IFilterToTypeConverter<decimal>, FilterToDecimalConverter>();
            this.Services.TryAddSingleton<IFilterToTypeConverter<double>, FilterToDoubleConverter>();
            this.Services.TryAddSingleton<IFilterToTypeConverter<float>, FilterToFloatConverter>();
            this.Services.TryAddSingleton<IFilterToTypeConverter<int>, FilterToIntegerConverter>();
            this.Services.TryAddSingleton<IFilterToTypeConverter<long>, FilterToLongConverter>();
            this.Services.TryAddSingleton<IFilterToTypeConverter<short>, FilterToShortConverter>();
        }
    }
}