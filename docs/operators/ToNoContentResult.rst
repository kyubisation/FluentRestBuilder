ToNoContentResult
---------------------------------------------------------------------------


Emits :code:`NoContentResult` on receiving a value. Does not contain the value.

Catches :code:`ValidationException` and converts it to
an appropriate :code:`IActionResult`.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<Microsoft.AspNetCore.Mvc.IActionResult> ToNoContentResult<TSource>(
        this IProviderObservable<TSource> observable)


