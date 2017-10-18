DoAsync
---------------------------------------------------------------------------


Asynchronously perform an action on the received value.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> DoAsync<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,System.Threading.Tasks.Task> action)


