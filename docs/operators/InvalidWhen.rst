InvalidWhen
---------------------------------------------------------------------------


If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhen<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,System.Boolean> invalidCheck,
        int statusCode,
        object error)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhen<TSource>(
        this IProviderObservable<TSource> observable,
        Func<System.Boolean> invalidCheck,
        int statusCode,
        Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhen<TSource>(
        this IProviderObservable<TSource> observable,
        Func<System.Boolean> invalidCheck,
        int statusCode,
        object error)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhen<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,System.Boolean> invalidCheck,
        int statusCode,
        Func<TSource,object> errorFactory)


