// <copyright file="LazyResolver.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Common
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class LazyResolver<TService> : Lazy<TService>
        where TService : class
    {
        public LazyResolver(IServiceProvider provider)
            : base(provider.GetService<TService>)
        {
        }
    }
}
