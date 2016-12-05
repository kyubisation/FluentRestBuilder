// <copyright file="IScopedStorage.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRest.Core.Storage
{
    public interface IScopedStorage<TValue>
    {
        TValue Value { get; set; }
    }
}
