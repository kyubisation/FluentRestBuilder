LoadCollection
---------------------------------------------------------------------------


Load a reference collection from the database.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> LoadCollection<TSource, TProperty>(
        this IProviderObservable<TSource> observable, Expression<Func<TSource,IEnumerable<TProperty>>> propertyExpression)


