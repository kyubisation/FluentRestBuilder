ForbiddenWhenAsync
---------------------------------------------------------------------------


If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,Task<System.Boolean>> invalidCheck,
        Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<TSource,Task<System.Boolean>> invalidCheck,
        object error)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<Task<System.Boolean>> invalidCheck,
        Func<TSource,object> errorFactory)



If the check returns :code:`true`, :code:`ValidationException`
is emitted as an error with the status code 403 (Forbidden).
Otherwise the given value is emitted.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> ForbiddenWhenAsync<TSource>(
        this IProviderObservable<TSource> observable,
        Func<Task<System.Boolean>> invalidCheck,
        object error)


