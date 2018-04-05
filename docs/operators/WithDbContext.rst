WithDbContext
---------------------------------------------------------------------------


Perform an action with the :code:`DbContext`.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> WithDbContext<TSource>(
        this IProviderObservable<TSource> observable,
        Action<TSource,Microsoft.EntityFrameworkCore.DbContext> action)


