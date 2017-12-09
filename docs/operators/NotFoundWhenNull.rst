NotFoundWhenNull
---------------------------------------------------------------------------


If the received value is null, :code:`ValidationException`
is emitted as an error with the status code 404 (Not Found).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> NotFoundWhenNull<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,object> errorFactory)


