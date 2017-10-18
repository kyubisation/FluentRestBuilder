WithEntityEntry
---------------------------------------------------------------------------


Perform an action with the :code:`EntityEntry<TSource>` of the received value.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> WithEntityEntry<TSource>(
        this IProviderObservable<TSource> observable, Action<EntityEntry<TSource>> action)


