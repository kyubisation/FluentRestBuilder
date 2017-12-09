// <copyright file="Disposable.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Disposables
{
    using System;

    public static class Disposable
    {
        public static readonly IDisposable Empty = new EmptyDisposable();

        private sealed class EmptyDisposable : IDisposable
        {
            public void Dispose()
            {
                // Do nothing
            }
        }
    }
}
