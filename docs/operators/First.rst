First
---------------------------------------------------------------------------


Emits the first element of a sequence.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> First<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Emits the first element of a sequence that satisfies a specified condition.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> First<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable,
        Expression<Func<TSource,System.Boolean>> predicate)


