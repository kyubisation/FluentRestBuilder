.. _observables-label:

Observables
===========

Observables can be subscribed to and can potentially emit values.
They can be completed, after which no more values will be emitted.
On error they will emit an Exception and afterwards can no longer emit a value
or be completed.

Observables could emit multiple values, however the observables from FluentRestBuilder
will only emit one value and complete afterwards or emit an exception
in an error case.

Observables can be awaited. This will return the last emitted value on completion
or throw the received exception in an error case.

.. include:: SingleObservable.rst
.. include:: AsyncSingleObservable.rst
.. include:: ControllerIntegration.rst
