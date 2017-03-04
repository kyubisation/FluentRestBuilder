// <copyright file="JsonMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.DistributedCache
{
    using System.Text;
    using Newtonsoft.Json;

    public class JsonMapper<TCacheEntry> : IByteMapper<TCacheEntry>
    {
        public byte[] ToByteArray(TCacheEntry cacheEntry)
        {
            var stringValue = JsonConvert.SerializeObject(cacheEntry);
            return Encoding.UTF8.GetBytes(stringValue);
        }

        public TCacheEntry FromByteArray(byte[] bytes)
        {
            try
            {
                var cacheValue = Encoding.UTF8.GetString(bytes);
                return JsonConvert.DeserializeObject<TCacheEntry>(cacheValue);
            }
            catch (JsonSerializationException exception)
            {
                throw new MappingException(exception.Message, exception);
            }
        }
    }
}
