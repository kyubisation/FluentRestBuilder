// <copyright file="FluentRestBuilderExtension.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using Builder;

    public static class FluentRestBuilderExtension
    {
        public static IFluentRestBuilder AddCachingPipes(
            this IFluentRestBuilder builder)
        {
            new FluentRestBuilderCore(builder.Services)
                .RegisterInputMemoryCachePipe()
                .RegisterInputDistributedCachePipe()
                .RegisterActionResultMemoryCachePipe()
                .RegisterActionResultDistributedCachePipe();
            return builder;
        }
    }
}
