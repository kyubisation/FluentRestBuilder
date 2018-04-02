WithDbContextAsync
---------------------------------------------------------------------------


Perform an async action with the :code:`DbContext`.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> WithDbContextAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,Microsoft.EntityFrameworkCore.DbContext,System.Threading.Tasks.Task> action)


