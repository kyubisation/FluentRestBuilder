// <copyright file="IMappingBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping
{
    using System;

    public interface IMappingBuilder<TInput>
    {
        IMappingBuilder<TInput> Embed<TEmbeddedResource, TMappedResource>(
            string name,
            Func<TInput, TEmbeddedResource> resourceSelector,
            Func<IMapperFactory<TEmbeddedResource>, IMapper<TEmbeddedResource, TMappedResource>>
                mapperSelector);

        Func<TInput, TOutput> UseMapper<TOutput>(
            Func<IMapperFactory<TInput>, IMapper<TInput, TOutput>> selection);
    }
}