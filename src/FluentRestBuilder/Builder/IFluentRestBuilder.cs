// <copyright file="IFluentRestBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Builder
{
    using System;
    using Hal;
    using Microsoft.Extensions.DependencyInjection;

    public interface IFluentRestBuilder
    {
        IServiceCollection Services { get; }

        IFluentRestBuilder AddRestMapper<TInput, TOutput>(
            Func<TInput, TOutput> mapping,
            Action<RestMapper<TInput, TOutput>> configuration = null)
            where TOutput : RestEntity;
    }
}