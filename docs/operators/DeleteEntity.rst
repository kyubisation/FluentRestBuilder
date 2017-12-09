DeleteEntity
---------------------------------------------------------------------------


Remove the received entity from the :code:`DbContext` and save the change.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> DeleteEntity<TSource>(
        this IProviderObservable<TSource> observable)


