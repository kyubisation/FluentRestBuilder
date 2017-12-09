ToOkObjectResult
---------------------------------------------------------------------------


Wrap the received value in an :code:`OkObjectResult`.

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToOkObjectResult<TSource>(
        this IProviderObservable<TSource> observable)


