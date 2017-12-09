CurrentUserHas
---------------------------------------------------------------------------


If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHas<TSource>(
        this IProviderObservable<TSource> observable,
        Func<System.Security.Claims.ClaimsPrincipal,TSource,System.Boolean> principalCheck,
        object error)



If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHas<TSource>(
        this IProviderObservable<TSource> observable,
        Func<System.Security.Claims.ClaimsPrincipal,System.Boolean> principalCheck,
        Func<TSource,object> errorFactory)



If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHas<TSource>(
        this IProviderObservable<TSource> observable,
        Func<System.Security.Claims.ClaimsPrincipal,System.Boolean> principalCheck,
        object error)



If the check returns :code:`false`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CurrentUserHas<TSource>(
        this IProviderObservable<TSource> observable,
        Func<System.Security.Claims.ClaimsPrincipal,TSource,System.Boolean> principalCheck,
        Func<TSource,object> errorFactory)


