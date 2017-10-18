ToActionResult
---------------------------------------------------------------------------


Convert the received value into an :code:`IActionResult`.

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToActionResult<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,Microsoft.AspNetCore.Mvc.IActionResult> mapping)


