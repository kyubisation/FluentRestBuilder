BadRequestWhenModelStateIsInvalid
---------------------------------------------------------------------------


If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 400 (Bad Request).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> BadRequestWhenModelStateIsInvalid<TSource>(
        this IProviderObservable<TSource> observable, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)


