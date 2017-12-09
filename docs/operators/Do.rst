Do
---------------------------------------------------------------------------


Perform an action on the received value.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> Do<TSource>(
        this IProviderObservable<TSource> observable,
        Action<TSource> action)


