ApplyFilterByClientRequest
---------------------------------------------------------------------------


Apply filter logic to the received :code:`IQueryable<TSource>`.

Matches the query parameters with the keys of the given filter dictionary.
Implement :code:`IFilterByClientRequestInterpreter` for custom behavior.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplyFilterByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable,
        Func<FilterExpressionProviderDictionary<TSource>,IFilterExpressionProviderDictionary<TSource>> factory)



Apply filter logic to the received :code:`IQueryable<TSource>`.
Tries to resolve :code:`IFilterExpressionProviderDictionary<TSource>`
via :code:`IServiceProvider`.

Matches the query parameters with the keys of the given filter dictionary.
Implement :code:`IFilterByClientRequestInterpreter` for custom behavior.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplyFilterByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Apply filter logic to the received :code:`IQueryable<TSource>`.

Matches the query parameters with the keys of the given filter dictionary.
Implement :code:`IFilterByClientRequestInterpreter` for custom behavior.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplyFilterByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable,
        IDictionary<string,IFilterExpressionProvider<TSource>> filterDictionary)


