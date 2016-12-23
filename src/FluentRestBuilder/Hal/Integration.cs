// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Hal
{
    using System;
    using Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static class Integration
    {
        public static IServiceCollection AddRestMapper<TInput, TOutput>(
            this IServiceCollection serviceCollection,
            Func<TInput, TOutput> mapping,
            Action<RestMapper<TInput, TOutput>> configuration = null)
            where TOutput : RestEntity
        {
            serviceCollection.AddScoped<IMapper<TInput, TOutput>>(
                serviceProvider =>
                {
                    var urlHelper = serviceProvider.GetService<IUrlHelper>();
                    var mapper = new RestMapper<TInput, TOutput>(mapping, urlHelper);
                    configuration?.Invoke(mapper);
                    return mapper;
                });
            return serviceCollection;
        }
    }
}
