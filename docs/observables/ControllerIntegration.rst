Controller Integration
----------------------

FluentRestBuilder provides extension methods that can be used to create an observable in a controller.
These use the service provider from the controller, so 

**FluentRestBuilder**

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CreateSingle<TSource>(
        this ControllerBase controller, TSource value)

    public static IProviderObservable<TSource> CreateAsyncSingle<TSource>(
        this ControllerBase controller, Func<Task<TSource>> valueFactory)

    public static IProviderObservable<TSource> CreateAsyncSingle<TSource>(
        this ControllerBase controller, Func<TSource> valueFactory)

    public static IProviderObservable<TSource> CreateAsyncSingle<TSource>(
        this ControllerBase controller, Lazy<TSource> valueFactory)

**FluentRestBuilder.EntityFrameworkCore**

In order for these to work, the entity framework :code:`DbContext` has
to be registered for FluentRestBuilder.

See :ref:`getting-started-label` for an example.

.. sourcecode:: csharp

    public static IProviderObservable<TSource> CreateEntitySingle<TSource>(
        this ControllerBase controller, Expression<Func<TSource, bool>> predicate)

    public static IProviderObservable<TSource> CreateEntitySingle<TSource>(
        this ControllerBase controller, params object[] keyValues)

    public static IProviderObservable<IQueryable<TSource>> CreateQueryableSingle<TSource>(
        this ControllerBase controller)


