CacheInMemoryCache
---------------------------------------------------------------------------


Cache the received value in :code:`IMemoryCache` with the given key
and the defined absolute expiration moment.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
        this IProviderObservable<TSource> observable, object key, System.DateTimeOffset absoluteExpiration)



Cache the received value in :code:`IMemoryCache` with the given key
and the defined absolute expiration moment relative to now.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
        this IProviderObservable<TSource> observable, object key, System.TimeSpan absoluteExpirationRelativeToNow)



Cache the received value in :code:`IMemoryCache` with the given key
and the given expiration token.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
        this IProviderObservable<TSource> observable, object key, Microsoft.Extensions.Primitives.IChangeToken expirationToken)



Cache the received value in :code:`IMemoryCache` with the given key
and the defined memory cache options factory function.
If an entry with the given key is found in the cache, it will be emitted
and the previous chain is skipped.

**Package:** FluentRestBuilder.Caching

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CacheInMemoryCache<TSource>(
        this IProviderObservable<TSource> observable, object key, Func<TSource,Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions> optionsFactory)


