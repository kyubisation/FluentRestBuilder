ThenInclude
---------------------------------------------------------------------------


Specifies additional related data to be further included based on a
related type that was just included.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<IIncludableQueryable<TSource,TProperty>> ThenInclude<TSource, TPreviousProperty, TProperty>(
        this IProviderObservable<IIncludableQueryable<TSource,TPreviousProperty>> observable,
        Expression<Func<TPreviousProperty,TProperty>> navigationPropertyPath)


