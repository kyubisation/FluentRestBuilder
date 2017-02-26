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

        /// <summary>
        /// Asynchronously map the input to the desired output.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="mapping">The asynchronous mapping function.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, Task<TOutput>> mapping)
            where TInput : class
            where TOutput : class
        {
            var factory = pipe.GetService<IMappingPipeFactory<TInput, TOutput>>();
            Check.IsPipeRegistered(factory, typeof(MappingPipe<,>));
            return factory.Create(mapping, pipe);
        }

        /// <summary>
        /// Map the input to the desired output.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="mapping">The mapping function.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> Map<TInput, TOutput>(
            this IOutputPipe<TInput> pipe, Func<TInput, TOutput> mapping)
            where TInput : class
            where TOutput : class =>
            pipe.Map(i => Task.FromResult(mapping(i)));
    }
}
