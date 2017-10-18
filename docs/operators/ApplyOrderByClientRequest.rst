ApplyOrderByClientRequest
---------------------------------------------------------------------------


Apply order by logic to the received :code:`IQueryable<TSource>`.

The default query parameter key is "sort".
A comma-separated list of properties is supported.
Prefix the property with "-" to sort descending.
Implement :code:`IOrderByClientRequestInterpreter` for custom behavior.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplyOrderByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable, Func<OrderByExpressionDictionary<TSource>,IOrderByExpressionDictionary<TSource>> factory)



Apply order by logic to the received :code:`IQueryable<TSource>`.
Tries to resolve :code:`IOrderByExpressionDictionary<TSource>`
via :code:`IServiceProvider`.

The default query parameter key is "sort".
A comma-separated list of properties is supported.
Prefix the property with "-" to sort descending.
Implement :code:`IOrderByClientRequestInterpreter` for custom behavior.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplyOrderByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable)



Apply order by logic to the received :code:`IQueryable<TSource>`.
Provide a dictionary with provided order by expressions.

The default query parameter key is "sort".
A comma-separated list of properties is supported.
Prefix the property with "-" to sort descending.
Implement :code:`IOrderByClientRequestInterpreter` for custom behavior.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplyOrderByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable, IDictionary<string,IOrderByExpressionFactory<TSource>> orderByExpressions)


