WithEntityEntryAsync
---------------------------------------------------------------------------


Perform an async action with the :code:`EntityEntry<TSource>`
of the received value.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> WithEntityEntryAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<EntityEntry<TSource>,System.Threading.Tasks.Task> action)


