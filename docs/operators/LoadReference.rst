LoadReference
---------------------------------------------------------------------------


Load a single reference from the database.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> LoadReference<TSource, TProperty>(
        this IProviderObservable<TSource> observable,
        Expression<Func<TSource,TProperty>> propertyExpression)


