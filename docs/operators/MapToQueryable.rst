MapToQueryable
---------------------------------------------------------------------------


Map to a :code:`IQueryable<TSource>` from the received :code:`DbContext`.
Use the :code:`Set`<TSource>` method to select the appropriate
:code:`IQueryable<TSource>`.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TTarget>> MapToQueryable<TSource, TTarget>(
        this IProviderObservable<TSource> observable, Func<Microsoft.EntityFrameworkCore.DbContext,IQueryable<TTarget>> mapping)



Map to a :code:`IQueryable<TSource>` from the received :code:`DbContext`.
Use the :code:`Set`<TSource>` method to select the appropriate
:code:`IQueryable<TSource>`.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TTarget>> MapToQueryable<TSource, TTarget>(
        this IProviderObservable<TSource> observable, Func<TSource,Microsoft.EntityFrameworkCore.DbContext,IQueryable<TTarget>> mapping)


