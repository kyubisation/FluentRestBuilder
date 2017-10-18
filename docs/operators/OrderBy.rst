OrderBy
---------------------------------------------------------------------------


Sorts the elements of a sequence in ascending order according to a key.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IOrderedQueryable<TSource>> OrderBy<TSource, TKey>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,TKey>> keySelector)



Sorts the elements of a sequence in ascending order by using a specified comparer.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IOrderedQueryable<TSource>> OrderBy<TSource, TKey>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,TKey>> keySelector, IComparer<TKey> comparer)


