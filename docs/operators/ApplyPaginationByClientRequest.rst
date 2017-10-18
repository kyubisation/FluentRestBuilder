ApplyPaginationByClientRequest
---------------------------------------------------------------------------


Configure the pagination capabilities.

WARNING: Do not use this before FilterByClientRequest, SearchByClientRequest or
OrderByClientRequest! This would result in erroneous pagination logic.

Requires :code:`HttpContextProviderAttribute` to be set.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<IQueryable<TSource>> ApplyPaginationByClientRequest<TSource>(
        this IProviderObservable<IQueryable<TSource>> observable, FluentRestBuilder.Operators.ClientRequest.PaginationOptions options)


