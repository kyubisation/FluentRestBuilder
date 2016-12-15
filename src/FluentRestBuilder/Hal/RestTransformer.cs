// <copyright file="RestTransformer.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Hal
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Transformers;

    public class RestTransformer<TInput, TOutput> : ITransformer<TInput, TOutput>
        where TOutput : RestEntity
    {
        private readonly IDictionary<string, object> embeddedResources = new Dictionary<string, object>();
        private readonly Func<TInput, TOutput> transformation;
        private readonly IUrlHelper urlHelper;

        public RestTransformer(
            Func<TInput, TOutput> transformation,
            IUrlHelper urlHelper)
        {
            this.transformation = transformation;
            this.urlHelper = urlHelper;
        }

        public TOutput Transform(TInput source)
        {
            var target = this.transformation(source);
            target.Links = this.GenerateLinks(target, source);
            if (this.embeddedResources.Count > 0)
            {
                target.Embedded = this.embeddedResources;
            }

            return target;
        }

        ITransformer<TInput, TOutput> ITransformer<TInput, TOutput>.Embed(string name, object value)
        {
            this.embeddedResources.Add(name, value);
            return this;
        }

        public RestTransformer<TInput, TOutput> Embed(string name, object value)
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
