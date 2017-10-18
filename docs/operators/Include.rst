Include
---------------------------------------------------------------------------


Specifies related entities to include in the query results. The navigation property
to be included is specified starting with the type of entity being queried
(<typeparamref name="TSource" />). If you wish to include additional types based on the
navigation properties of the type being included, then chain a call to
:code:`Func{`<TSource>,``2}})`
after this call.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<IIncludableQueryable<TSource,TProperty>> Include<TSource, TProperty>(
        this IProviderObservable<IQueryable<TSource>> observable, Expression<Func<TSource,TProperty>> navigationPropertyPath)


