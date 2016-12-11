// <copyright file="ScopedStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Storage
{
    public class ScopedStorage<TValue> : IScopedStorage<TValue>
    {
        public TValue Value { get; set; }
    }
}