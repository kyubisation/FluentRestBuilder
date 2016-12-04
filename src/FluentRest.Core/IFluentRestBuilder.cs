// <copyright file="IFluentRestBuilder.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IFluentRestBuilder
    {
        IServiceCollection Services { get; }
    }
}