ToList
---------------------------------------------------------------------------


Creates and emits a <see cref="T:System.Collections.Generic.List`1"></see>
from an <see cref="T:System.Collections.Generic.IEnumerable`1"></see>.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<List<TSource>> ToList<TSource>(
        this IProviderObservable<IEnumerable<TSource>> observable)


