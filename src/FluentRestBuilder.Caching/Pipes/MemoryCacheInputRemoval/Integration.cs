// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Builder;
    using Caching.Pipes.MemoryCacheInputRemoval;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterMemoryCacheInputRemovalPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddScoped(
                typeof(IMemoryCacheInputRemovalPipeFactory<>),
                typeof(MemoryCacheInputRemovalPipeFactory<>));
            return builder;
        }

        public static OutputPipe<TInput> RemoveFromMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, Func<TInput, object> keyFactory) =>
            pipe.GetRequiredService<IMemoryCacheInputRemovalPipeFactory<TInput>>()
                .Create(keyFactory, pipe);

        public static OutputPipe<TInput> RemoveFromMemoryCache<TInput>(
            this IOutputPipe<TInput> pipe, object key) =>
            pipe.RemoveFromMemoryCache(i => key);
    }
}
