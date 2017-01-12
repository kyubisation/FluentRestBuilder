// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using System.Threading.Tasks;
    using Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Pipes.Mapping;

    public static partial class Integration
    {
        public static IFluentRestBuilderCore RegisterMappingPipe(
            this IFluentRestBuilderCore builder)
        {
            builder.Services.TryAddSingleton(
                typeof(IMappingPipeFactory<,>), typeof(MappingPipeFactory<,>));
            return builder;
        }

        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task<TOutput>> mapping)
            where TInput : class
            where TOutput : class =>
            pipe.GetRequiredService<IMappingPipeFactory<TInput, TOutput>>()
                .Create(mapping, pipe);

        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, TOutput> mapping)
            where TInput : class
            where TOutput : class =>
            pipe.Map(i => Task.FromResult(mapping(i)));
    }
}
