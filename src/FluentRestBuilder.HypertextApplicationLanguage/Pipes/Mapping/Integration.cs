// <copyright file="Integration.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using HypertextApplicationLanguage.Mapping;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class Integration
    {
        /// <summary>
        /// Maps the input according to the selected <see cref="IMapper{TInput,TOutput}"/>.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="selection">The <see cref="IMapper{TInput,TOutput}"/> selection.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> UseMapper<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<IMapperFactory<TInput>, IMapper<TInput, TOutput>> selection)
            where TInput : class
            where TOutput : class
        {
            var mapper = pipe.GetService<IMapperFactory<TInput>>();
            return pipe.Map(i => selection(mapper).Map(i));
        }

        /// <summary>
        /// Maps the input according to the built mapping function.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TOutput">The output type.</typeparam>
        /// <param name="pipe">The parent pipe.</param>
        /// <param name="builder">The builder for the mapping.</param>
        /// <returns>An output pipe to continue with.</returns>
        public static OutputPipe<TOutput> BuildMapping<TInput, TOutput>(
            this IOutputPipe<TInput> pipe,
            Func<IMappingBuilder<TInput>, Func<TInput, TOutput>> builder)
            where TInput : class
            where TOutput : class
        {
            var mappingBuilder = pipe.GetService<IMappingBuilder<TInput>>();
            return pipe.Map(builder(mappingBuilder));
        }
    }
}
