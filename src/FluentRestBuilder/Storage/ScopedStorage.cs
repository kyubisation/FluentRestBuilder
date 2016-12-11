// <copyright file="ScopedStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Storage
{
    public class ScopedStorage<TValue> : IScopedStorage<TValue>
    {
        public TValue Value { get; set; }
    }
}