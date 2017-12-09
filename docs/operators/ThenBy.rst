ThenBy
---------------------------------------------------------------------------


Performs a subsequent ordering of the elements in a sequence
in ascending order according to a key.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IOrderedQueryable<TSource>> ThenBy<TSource, TKey>(
        this IProviderObservable<IOrderedQueryable<TSource>> observable,
        Expression<Func<TSource,TKey>> keySelector)



Performs a subsequent ordering of the elements in a sequence
in ascending order by using a specified comparer.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IOrderedQueryable<TSource>> ThenBy<TSource, TKey>(
        this IProviderObservable<IOrderedQueryable<TSource>> observable,
        Expression<Func<TSource,TKey>> keySelector,
        IComparer<TKey> comparer)


