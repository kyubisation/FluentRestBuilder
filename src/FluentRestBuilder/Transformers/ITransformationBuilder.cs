// <copyright file="ITransformationBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Transformers
{
    using System;

    public interface ITransformationBuilder<TInput>
    {
        ITransformationBuilder<TInput> Embed<TEmbeddedResource, TTransformedResource>(
            string name,
            Func<TInput, TEmbeddedResource> resourceSelector,
            Func<
                    ITransformerFactory<TEmbeddedResource>,
                    ITransformer<TEmbeddedResource, TTransformedResource>>
                transformerSelector);

        Func<TInput, TOutput> UseTransformer<TOutput>(
            Func<ITransformerFactory<TInput>, ITransformer<TInput, TOutput>> selection);
    }
}