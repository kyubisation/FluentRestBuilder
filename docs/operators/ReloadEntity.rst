ReloadEntity
---------------------------------------------------------------------------


Reloads the entity from the database overwriting any property
values with values from the database.

The entity will be in the
:code:`Unchanged` state
after calling this method, unless the entity does not exist in the database,
in which case the entity will be
:code:`Detached`. Finally,
calling Reload on an :code:`Added`
entity that does not exist in the database is a no-op. Note, however, that
an Added entity may not yet have had its permanent key value created.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ReloadEntity<TSource>(
        this IProviderObservable<TSource> observable)


