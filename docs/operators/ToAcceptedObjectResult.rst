ToAcceptedObjectResult
---------------------------------------------------------------------------


Wrap the received value in an :code:`ObjectResult`
with status code 202 (Accepted).

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToAcceptedObjectResult<TSource>(
        this IProviderObservable<TSource> observable)


