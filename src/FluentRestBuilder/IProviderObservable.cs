// <copyright file="IProviderObservable.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;

    public interface IProviderObservable<out T> : IObservable<T>
    {
        IServiceProvider ServiceProvider { get; }
    }
}
