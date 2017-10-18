SingleAsync
---------------------------------------------------------------------------


Emits the only element of a sequence, and throws an exception
if there is not exactly one element in the sequence.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> SingleAsync<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Emits the only element of a sequence that satisfies a specified
condition, and throws an exception if more than one such element exists.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> SingleAsync<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable,
        Expression<Func<TSource,System.Boolean>> predicate)


