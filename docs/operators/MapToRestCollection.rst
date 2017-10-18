MapToRestCollection
---------------------------------------------------------------------------


Maps the entries of the received :code:`IEnumerable<TSource>`
according to the given mapping function and wraps the result
in an :code:`IRestEntity`.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder.HypertextApplicationLanguage

.. sourcecode:: csharp

    public static IProviderObservable<FluentRestBuilder.HypertextApplicationLanguage.IRestEntity> MapToRestCollection<TSource, TTarget>(
        this IProviderObservable<IEnumerable<TSource>> observable,
        Func<TSource,TTarget> mapping)


