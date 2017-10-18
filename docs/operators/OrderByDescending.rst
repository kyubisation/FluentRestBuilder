OrderByDescending
---------------------------------------------------------------------------


Sorts the elements of a sequence in descending order according to a key.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IOrderedQueryable<TSource>> OrderByDescending<TSource, TKey>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,TKey>> keySelector)



Sorts the elements of a sequence in descending order by using a specified comparer.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IOrderedQueryable<TSource>> OrderByDescending<TSource, TKey>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,TKey>> keySelector, IComparer<TKey> comparer)


