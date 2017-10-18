ForbiddenWhen
---------------------------------------------------------------------------


If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,System.Boolean> invalidCheck, Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,System.Boolean> invalidCheck, object error)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
        this IProviderObservable<TSource> observable, Func<System.Boolean> invalidCheck, Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhen<TSource>(
        this IProviderObservable<TSource> observable, Func<System.Boolean> invalidCheck, object error)


