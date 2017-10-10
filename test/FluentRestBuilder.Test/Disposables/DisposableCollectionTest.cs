// <copyright file="DisposableCollectionTest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Test.Disposables
{
    using System;
    using FluentRestBuilder.Disposables;
    using Xunit;

    public class DisposableCollectionTest
    {
        [Fact]
        public void TestDisposing()
        {
            var disposable = new Disposable();
            var disposables = new DisposableCollection(disposable);
            disposables.Dispose();
            Assert.True(disposable.IsDisposed);
        }

        [Fact]
        public void TestPostDisposeAdd()
        {
            var disposables = new DisposableCollection();
            disposables.Dispose();
            var disposable = new Disposable();
            disposables.Add(disposable);
            Assert.True(disposable.IsDisposed);
        }

        private sealed class Disposable : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                this.IsDisposed = true;
            }
        }
    }
}
