// <copyright file="LazyResolver.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Common
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
