AsyncSingleObservable<T>
------------------------

This observable can be instantiated by providing a callback, which is only executed
once the observable is subscribed to.

.. sourcecode:: csharp

    public AsyncSingleObservable(Func<Task<T>> valueTaskFactory, IServiceProvider serviceProvider)
    public AsyncSingleObservable(Func<T> valueFactory, IServiceProvider serviceProvider)
    public AsyncSingleObservable(Lazy<T> lazyValue, IServiceProvider serviceProvider)

**Examples**

.. sourcecode:: csharp

    new AsyncSingleObservable<string>(() => "example", serviceProvider)
    new AsyncSingleObservable<string>(async () => await AsynchronousTask(), serviceProvider)
    
    // Alternatively
    Observable.AsyncSingle(() => "example", serviceProvider)
    Observable.AsyncSingle(async () => await AsynchronousTask(), serviceProvider)
    