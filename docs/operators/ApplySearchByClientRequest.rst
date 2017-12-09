ApplySearchByClientRequest
---------------------------------------------------------------------------


Apply a global search to the received :code:`IQueryable<TSource>`.

The default query parameter key is "q".
Implement :code:`ISearchByClientRequestInterpreter` for custom behavior.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplySearchByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable,
        Func<string,Expression<Func<TSource,System.Boolean>>> searchExpression)


