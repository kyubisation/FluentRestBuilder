SingleObservable<T>
-------------------

This is an observable that emits the value that it was given on instancing and completes.

.. sourcecode:: csharp

    public SingleObservable(T value, IServiceProvider serviceProvider)

**Examples**

.. sourcecode:: csharp

    new SingleObservable<string>("example", serviceProvider)

    // Alternatively
    Observable.Single("example", serviceProvider)