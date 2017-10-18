ToOptionsResult
---------------------------------------------------------------------------


Emits an :code:`OptionsResult` which lists the allowed
HTTP verbs.

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToOptionsResult<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,IEnumerable<FluentRestBuilder.HttpVerb>> verbsFactory)



Emits an :code:`OptionsResult` which lists the allowed
HTTP verbs.

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

Requires usage of :code:`HttpContextProviderAttribute`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToOptionsResult<TSource>(
        this IProviderObservable<TSource> observable, Func<AllowedOptionsBuilder<TSource>,AllowedOptionsBuilder<TSource>> factory)


