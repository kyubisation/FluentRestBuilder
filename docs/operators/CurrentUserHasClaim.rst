CurrentUserHasClaim
---------------------------------------------------------------------------


If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
        this IProviderObservable<TSource> observable, string claimType, string claim, Func<TSource,object> errorFactory)



If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
        this IProviderObservable<TSource> observable, string claimType, string claim, object error)



If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
        this IProviderObservable<TSource> observable, string claimType, Func<TSource,string> claimFactory, Func<TSource,object> errorFactory)



If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHasClaim<TSource>(
        this IProviderObservable<TSource> observable, string claimType, Func<TSource,string> claimFactory, object error)


