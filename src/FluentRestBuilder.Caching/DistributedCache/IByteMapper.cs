// <copyright file="IByteMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.DistributedCache
{
    public interface IByteMapper<TCacheEntry>
    {
        byte[] ToByteArray(TCacheEntry cacheEntry);

        TCacheEntry FromByteArray(byte[] bytes);
    }
}
