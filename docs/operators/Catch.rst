Catch
---------------------------------------------------------------------------


Catch an exception emitted from the previous observables or operators
and return a new observable.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> Catch<TSource>(
        this IProviderObservable<TSource> observable,
        Func<System.Exception,IProviderObservable<TSource>> handler)



Catch an exception emitted from the previous observables or operators
and perform an action with it. This will only catch the exception if it is
an instance of the declared exception type.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> Catch<TSource, TException>(
        this IProviderObservable<TSource> observable,
        Action<TException> action)



Catch an exception emitted from the previous observables or operators
and perform an action with it.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> Catch<TSource>(
        this IProviderObservable<TSource> observable,
        Action<System.Exception> action)



Catch an exception emitted from the previous observables or operators
and return a new observable. This will only catch the exception if it is
an instance of the declared exception type.

**Package:** FluentRestBuilder

.. sourcecode:: csharp

    public static IProviderObservable<TSource> Catch<TSource, TException>(
        this IProviderObservable<TSource> observable,
        Func<TException,IProviderObservable<TSource>> handler)


