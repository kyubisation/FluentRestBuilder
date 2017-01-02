﻿// <copyright file="RestMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.HypertextApplicationLanguage.Mapping
{
    using System;
    using System.Collections.Generic;
    using HypertextApplicationLanguage;
    using Links;
    using Microsoft.AspNetCore.Mvc;

    public class RestMapper<TInput, TOutput> : IMapper<TInput, TOutput>
        where TOutput : RestEntity
    {
        private readonly IDictionary<string, object> embeddedResources = new Dictionary<string, object>();
        private readonly Func<TInput, TOutput> mapping;
        private readonly IUrlHelper urlHelper;

        public RestMapper(
            Func<TInput, TOutput> mapping,
            IUrlHelper urlHelper)
        {
            this.mapping = mapping;
            this.urlHelper = urlHelper;
        }

        public TOutput Map(TInput source)
        {
            var target = this.mapping(source);
            target.Links = this.GenerateLinks(target, source);
            if (this.embeddedResources.Count > 0)
            {
                target.Embedded = this.embeddedResources;
            }

            return target;
        }

        IMapper<TInput, TOutput> IMapper<TInput, TOutput>.Embed(string name, object value)
        {
            this.embeddedResources.Add(name, value);
            return this;
        }

        public RestMapper<TInput, TOutput> Embed(string name, object value)
        {
            this.embeddedResources.Add(name, value);
            return this;
        }

        private IDictionary<string, object> GenerateLinks(
            TOutput target, TInput source)
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            // ReSharper disable once SuspiciousTypeConversion.Global
            var generator = target as ILinkGenerator<TInput>;

            // ReSharper disable once ExpressionIsAlwaysNull
            return generator != null ? this.GenerateLinks(generator, source) : null;
        }

        private IDictionary<string, object> GenerateLinks(
            ILinkGenerator<TInput> generator, TInput source)
        {
            var links = generator.GenerateLinks(this.urlHelper, source);
            return NamedLink.BuildLinks(links);
        }
    }
}