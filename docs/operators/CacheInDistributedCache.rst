CacheInDistributedCache
---------------------------------------------------------------------------


Cache the received value in :code:`IDistributedCache` with the given key.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
        this IProviderObservable<TSource> observable, string key)



Cache the received value in :code:`IDistributedCache` with the given key
and the defined absolute expiration moment.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
        this IProviderObservable<TSource> observable, string key, System.DateTimeOffset absoluteExpiration)



Cache the received value in :code:`IDistributedCache` with the given key
and the defined absolute expiration moment.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
        this IProviderObservable<TSource> observable, string key, System.TimeSpan absoluteExpirationRelativeToNow)



Cache the received value in :code:`IDistributedCache` with the given key
and the defined distributed cache options factory function.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInDistributedCache<TSource>(
        this IProviderObservable<TSource> observable, string key, Func<TSource,Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions> optionsFactory)


