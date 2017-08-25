// <copyright file="IProviderObservable.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder
{
    using System;

    public interface IProviderObservable<out T> : IServiceProvider, IObservable<T>
    {
    }
}
