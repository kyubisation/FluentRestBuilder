MapAsync
---------------------------------------------------------------------------


Asynchronously map the received value to the desired output.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TTarget> MapAsync<TSource, TTarget>(
        this IProviderObservable<TSource> observable,
        Func<TSource,Task<TTarget>> mapping)


