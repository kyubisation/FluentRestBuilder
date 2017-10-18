InvalidWhenAsync
---------------------------------------------------------------------------


If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhenAsync<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,Task<System.Boolean>> invalidCheck, int statusCode, object error)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhenAsync<TSource>(
        this IProviderObservable<TSource> observable, Func<Task<System.Boolean>> invalidCheck, int statusCode, Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhenAsync<TSource>(
        this IProviderObservable<TSource> observable, Func<Task<System.Boolean>> invalidCheck, int statusCode, object error)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the given status code.
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> InvalidWhenAsync<TSource>(
        this IProviderObservable<TSource> observable, Func<TSource,Task<System.Boolean>> invalidCheck, int statusCode, Func<TSource,object> errorFactory)


