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
        private readonly IServiceProvider serviceProvider;

        public SingleObservable(T value, IServiceProvider serviceProvider)
        {
            Check.IsNull(serviceProvider, nameof(serviceProvider));
            this.value = value;
            this.serviceProvider = serviceProvider;
        }

        object IServiceProvider.GetService(Type serviceType) =>
            this.serviceProvider.GetService(serviceType);

        IDisposable IObservable<T>.Subscribe(IObserver<T> observer)
        {
            Check.IsNull(observer, nameof(observer));
            observer.OnNext(this.value);
            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}
