// <copyright file="DisposableCollection.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Disposables
{
    using System;
    using System.Collections.Immutable;
    using System.Threading;

    public class DisposableCollection : IDisposable
    {
        private ImmutableList<IDisposable> disposables = ImmutableList<IDisposable>.Empty;

        public DisposableCollection(params IDisposable[] disposables)
        {
            if (disposables.Length == 0)
            {
                return;
            }

            this.disposables = this.disposables.AddRange(disposables);
        }

        public void Add(IDisposable disposable)
        {
            if (this.disposables == null)
            {
                disposable.Dispose();
                return;
            }

            this.disposables = this.disposables.Add(disposable);
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref this.disposables, null)?.ForEach(d => d.Dispose());
        }
    }
}
