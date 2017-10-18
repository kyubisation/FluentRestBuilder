Where
---------------------------------------------------------------------------


Filters a sequence of values based on a predicate.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> Where<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,System.Boolean>> predicate)



Filters a sequence of values based on a predicate.
Each element's index is used in the logic of the predicate function.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> Where<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,int,System.Boolean>> predicate)


