ToCreatedAtRouteResult
---------------------------------------------------------------------------


Wrap the received value in an :code:`CreatedAtRouteResult`
with status code 201 (Created).

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToCreatedAtRouteResult<TSource>(
        this IProviderObservable<TSource> observable, string routeName, Func<TSource,object> routeValuesFactory)



Wrap the received value in an :code:`CreatedAtRouteResult`
with status code 201 (Created).

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToCreatedAtRouteResult<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,object> routeValuesFactory)


