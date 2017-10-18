FirstAsync
---------------------------------------------------------------------------


Emits the first element of a sequence.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> FirstAsync<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Emits the first element of a sequence that satisfies a specified condition.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> FirstAsync<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,System.Boolean>> predicate)


