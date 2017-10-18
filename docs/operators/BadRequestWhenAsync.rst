BadRequestWhenAsync
---------------------------------------------------------------------------


If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 400 (Bad Request).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> BadRequestWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,Task<System.Boolean>> invalidCheck,
        Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 400 (Bad Request).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> BadRequestWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,Task<System.Boolean>> invalidCheck,
        object error)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 400 (Bad Request).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> BadRequestWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<Task<System.Boolean>> invalidCheck,
        Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 400 (Bad Request).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> BadRequestWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<Task<System.Boolean>> invalidCheck,
        object error)


