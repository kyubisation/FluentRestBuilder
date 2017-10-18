Map
---------------------------------------------------------------------------


Map the received value to the desired output.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TTarget> Map<TSource, TTarget>(
        this IProviderObservable<TSource> observable, Func<TSource,TTarget> mapping)


