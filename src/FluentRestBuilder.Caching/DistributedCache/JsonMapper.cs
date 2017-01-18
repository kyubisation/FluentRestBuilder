// <copyright file="JsonMapper.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Caching.DistributedCache
{
    using Newtonsoft.Json;

    public class JsonMapper<TCacheEntry> : StringMapperBase<TCacheEntry>
    {
        protected override string ToString(TCacheEntry cacheEntry) =>
            JsonConvert.SerializeObject(cacheEntry);

        protected override TCacheEntry FromString(string cacheValue)
        {
            try
            {
                return JsonConvert.DeserializeObject<TCacheEntry>(cacheValue);
            }
            catch (JsonSerializationException exception)
            {
                throw new MappingException(exception.Message, exception);
            }
        }
    }
}
