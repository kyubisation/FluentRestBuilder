AsNoTracking
---------------------------------------------------------------------------


Returns a new query where the change tracker will not track any of the entities
that are returned. If the entity instances are modified, this will not be
detected by the change tracker and
:code:`SaveChanges` will not
persist those changes to the database.

Disabling change tracking is useful for read-only scenarios because it avoids
the overhead of setting up change tracking for each entity instance. You should
not disable change tracking if you want to manipulate entity instances and
persist those changes to the database using
:code:`SaveChanges`.

Identity resolution will still be performed to ensure that all occurrences of
an entity with a given key in the result set are represented by the same entity
instance.

The default tracking behavior for queries can be controlled by
:code:`QueryTrackingBehavior`.

**Package:** FluentRestBuilder.EntityFrameworkCore

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> AsNoTracking<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)


