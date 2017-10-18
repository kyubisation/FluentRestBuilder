Single
---------------------------------------------------------------------------


Emits the only element of a sequence, and throws an exception
if there is not exactly one element in the sequence.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> Single<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Emits the only element of a sequence that satisfies a specified
condition, and throws an exception if more than one such element exists.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> Single<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable,
        Expression<Func<TSource,System.Boolean>> predicate)


