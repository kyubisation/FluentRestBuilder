SingleOrDefaultAsync
---------------------------------------------------------------------------


Emits  the only element of a sequence, or a default value
if the sequence is empty; this method throws an exception
if there is more than one element in the sequence.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> SingleOrDefaultAsync<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Emits the only element of a sequence that satisfies a specified
condition or a default value if no such element exists; this
method throws an exception if more than one element satisfies the condition.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> SingleOrDefaultAsync<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable,
        Expression<Func<TSource,System.Boolean>> predicate)


