FirstOrDefault
---------------------------------------------------------------------------


Emits the first element of a sequence, or a default
value if the sequence contains no elements.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> FirstOrDefault<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Emits the first element of a sequence that satisfies a
specified condition or a default value if no such element is found.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> FirstOrDefault<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,System.Boolean>> predicate)


