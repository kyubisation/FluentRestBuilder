// <copyright file="SingleObservable.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Observables
{
    using System;
    using Disposables;

    public sealed class SingleObservable<T> : IProviderObservable<T>
    {
        private readonly T value;

        public SingleObservable(T value, IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.value = value;
        }

        public IServiceProvider ServiceProvider { get; }

        IDisposable IObservable<T>.Subscribe(IObserver<T> observer)
        {
            Check.IsNull(observer, nameof(observer));
            observer.OnNext(this.value);
            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}
