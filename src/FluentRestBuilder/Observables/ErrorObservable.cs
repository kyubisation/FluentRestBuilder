// <copyright file="ErrorObservable.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace FluentRestBuilder
{
    using System;
    using Disposables;

    public sealed class ErrorObservable<T> : IProviderObservable<T>
    {
        private readonly Exception exception;

        public ErrorObservable(Exception exception, IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.exception = exception;
        }

        public IServiceProvider ServiceProvider { get; }

        IDisposable IObservable<T>.Subscribe(IObserver<T> observer)
        {
            Check.IsNull(observer, nameof(observer));
            observer.OnError(this.exception);
            return Disposable.Empty;
        }
    }
}
