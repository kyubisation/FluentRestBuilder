ToListAsync
---------------------------------------------------------------------------


Asynchronously creates a :code:`List<TSource>`
from an :code:`IQueryable<TSource>` by enumerating it asynchronously.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<List<TSource>> ToListAsync<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)


