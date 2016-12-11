// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Transformers.Hal
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static class Integration
    {
        public static IServiceCollection AddRestTransformer<TInput, TOutput>(
            this IServiceCollection serviceCollection,
            Func<TInput, TOutput> transformation,
            Action<RestTransformer<TInput, TOutput>> configuration = null)
            where TOutput : RestEntity
        {
            serviceCollection.AddScoped<ITransformer<TInput, TOutput>>(
                serviceProvider =>
                {
                    var urlHelper = serviceProvider.GetService<IUrlHelper>();
                    var transformer = new RestTransformer<TInput, TOutput>(transformation, urlHelper);
                    configuration?.Invoke(transformer);
                    return transformer;
                });
            return serviceCollection;
        }
    }
}
