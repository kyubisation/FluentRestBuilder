// <copyright file="FluentRestBuilderConfiguration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Operators.ClientRequest.Interpreters;
    using Storage;

    public class FluentRestBuilderConfiguration : IFluentRestBuilderConfiguration
    {
        public FluentRestBuilderConfiguration(IServiceCollection services)
        {
            this.Services = services;
            this.Services.TryAddScoped(typeof(IScopedStorage<>), typeof(ScopedStorage<>));
            this.Services.TryAddScoped<
                IFilterByClientRequestInterpreter, FilterByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                IOrderByClientRequestInterpreter, OrderByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                IPaginationByClientRequestInterpreter, PaginationByClientRequestInterpreter>();
            this.Services.TryAddScoped<
                ISearchByClientRequestInterpreter, SearchByClientRequestInterpreter>();
        }

        public IServiceCollection Services { get; }
    }
}