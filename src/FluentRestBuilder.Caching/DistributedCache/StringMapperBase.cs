// <copyright file="StringMapperBase.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.DistributedCache
{
    using System.Text;

    public abstract class StringMapperBase<TCacheEntry> : IByteMapper<TCacheEntry>
    {
        protected Encoding Encoding { get; set; } = Encoding.UTF8;

        public byte[] ToByteArray(TCacheEntry cacheEntry) =>
            this.Encoding.GetBytes(this.ToString(cacheEntry));

        public TCacheEntry FromByteArray(byte[] bytes) =>
            this.FromString(this.Encoding.GetString(bytes));

        protected abstract string ToString(TCacheEntry cacheEntry);

        protected abstract TCacheEntry FromString(string cacheValue);
    }
}
