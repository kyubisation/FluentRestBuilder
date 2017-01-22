// <copyright file="ReflectionRestMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping
{
    using FluentRestBuilder.Storage;
    using Links;
    using Microsoft.AspNetCore.Mvc;
    using Reflection;

    public class ReflectionRestMapper<TInput, TOutput> : RestMapper<TInput, TOutput>
        where TOutput : RestEntity
    {
        public ReflectionRestMapper(
            IReflectionMapper<TInput, TOutput> reflectionMapper,
            IScopedStorage<IUrlHelper> urlHelper,
            ILinkAggregator linkAggregator)
            : base(reflectionMapper.Map, urlHelper, linkAggregator)
        {
        }
    }
}
