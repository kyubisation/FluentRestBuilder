// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Pipes.Mapping;

    public static partial class Integration
    {
        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task<TOutput>> mapping)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<IMappingPipeFactory<TInput, TOutput>>()
                .Resolve(mapping, pipe);

        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, TOutput> mapping)
            where TInput : class
            where TOutput : class =>
            pipe.Map(i => Task.FromResult(mapping(i)));
    }
}
