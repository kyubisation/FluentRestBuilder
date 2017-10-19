Pagination
==========

FluentRestBuilder provides operators to implement pagination (filtering, sorting, offset and limit)
for :code:`IQueryable<TSource>`.

There are four pagination operators for different purposes.

=============================== ==================================================================
Operator                        Effect
=============================== ==================================================================
ApplyFilterByClientRequest      Applies Where clauses to the :code:`IQueryable<TSource>`
ApplyOrderByClientRequest       Applies OrderBy clauses to the :code:`IQueryable<TSource>`
ApplyPaginationByClientRequest  Applies Skip and Take to the :code:`IQueryable<TSource>`
ApplySearchByClientRequest      Applies a single Where clause to the :code:`IQueryable<TSource>`
=============================== ==================================================================


Naming Convention
-----------------

FluentRestBuilder tries to figure out the naming convention from the MVC JSON configuration.
By default (of MVC) this follows camelCasing.

Implement :code:`FluentRestBuilder.Json.IJsonPropertyNameResolver` and register it as a singleton
in order to provide your own name convention logic.

Interpreters
------------

Each operator has its own interpreter for interpreting a client request.


Filter Interpreter
~~~~~~~~~~~~~~~~~~

The default filter interpreter parses the query paramters of a request.
The interpreter (not the operator) supports the following filter types:

============================================== ==================================
Prefix (The filter value must start with this) Type

============================================== ==================================
                                               Default (Depends on configuration)
~                                              Contains
<=                                             LessThanOrEqual
>=                                             GreaterThanOrEqual
<                                              LessThan
>                                              GreaterThan
=                                              Equals
^=                                             StartsWith
$=                                             EndsWith
!=                                             NotEqual
============================================== ==================================

This means in order to search for a field value equal to :code:`=`,
the query parameter value must contain :code:`=%3D` (...&field==%3D&...),
as the actual search value (:code:`=`) must be encoded.

In order to implement your own filter interpreter implement
:code:`FluentRestBuilder.Operators.ClientRequest.Interpreters.IFilterByClientRequestInterpreter`.

**Example:**

...&fieldA=a&fieldC=~jo&fieldE=<5&fieldG=>=3&...


Order By Interpreter
~~~~~~~~~~~~~~~~~~~~

The default order by interpreter parses the query paramter "sort"
(or "Sort" depending on the naming convention).
The value is split by comma.
To order by descending prepend the field with "-".

In order to implement your own order by interpreter implement
:code:`FluentRestBuilder.Operators.ClientRequest.Interpreters.IOrderByClientRequestInterpreter`.

**Example:**

...&sort=fieldA,-fieldC,fieldF&...


Pagination Interpreter
~~~~~~~~~~~~~~~~~~~~~~

The default pagination interpreter parses the query parameters "limit" and "offset"
(or "Limit" and "Offset" depending on the naming convention) or the Range header.

**Range Header**:
:code:`Range: items=10-29` is the same as ...&offset=10&limit=20&...

In order to implement your own pagination interpreter implement
:code:`FluentRestBuilder.Operators.ClientRequest.Interpreters.IPaginationByClientRequestInterpreter`.


Search Interpreter
~~~~~~~~~~~~~~~~~~

The default search interpreter uses the query parameter "q"
(or "Q" depending on the naming convention).

In order to implement your own search interpreter implement
:code:`FluentRestBuilder.Operators.ClientRequest.Interpreters.ISearchByClientRequestInterpreter`.


Filter Providers
----------------

The :code:`ApplyFilterByClientRequest` operator requires an instance of
:code:`IDictionary<string, IFilterExpressionProvider<TSource>>` or
:code:`IFilterExpressionProviderDictionary<TSource>` (which is a wrapper for the first).

This can be achieved by either providing it to the operator directly as a parameter or
by adding a service as :code:`IFilterExpressionProviderDictionary<TSource>` to the
dependency injection container.

There are two extension methods on the :code:`IFluentRestBuilderConfiguration` to 
configure filter providers (and order by expressions) via reflection.

.. sourcecode:: csharp

    public void ConfigureServices(IServiceCollection services)
    {
        ...

        services.AddFluentRestBuilder()
            // Configures filter provider (and order by expressions) for an entity
            .ConfigureFiltersAndOrderByExpressionsForEntity<ExampleEntity>()
            // Configures filter providers (and order by expressions) for all
            // entities in the specified DbContext
            // Requires the FluentRestBuilder.EntityFrameworkCore package
            .AddEntityFrameworkCoreIntegration<ApplicationDbContext>() // Required for EF Core operators to work
            .ConfigureFiltersAndOrderByExpressionsForDbContextEntities<ApplicationDbContext>();
    }


Alternatively the :code:`FilterExpressionProviderDictionary<TSource>` class can be used,
either in the :code:`ApplyFilterByClientRequest` overload or when registering it as
a service in the dependency injection container.


Order By Expressions
--------------------

The :code:`ApplyOrderByClientRequest` operator requires an instance of
:code:`IDictionary<string, IOrderByExpressionFactory<TSource>>` or
:code:`IOrderByExpressionDictionary<TSource>` (which is a wrapper for the first).

This can be achieved by either providing it to the operator directly as a parameter or
by adding a service as :code:`IOrderByExpressionDictionary<TSource>` to the
dependency injection container.

There are two extension methods on the :code:`IFluentRestBuilderConfiguration` to 
configure order by expressions (and filter providers) via reflection.

.. sourcecode:: csharp

    public void ConfigureServices(IServiceCollection services)
    {
        ...

        services.AddFluentRestBuilder()
            // Configures order by expressions (and filter providers) for an entity
            .ConfigureFiltersAndOrderByExpressionsForEntity<ExampleEntity>()
            // Configures order by expressions (and filter providers) for all
            // entities in the specified DbContext
            // Requires the FluentRestBuilder.EntityFrameworkCore package
            .AddEntityFrameworkCoreIntegration<ApplicationDbContext>() // Required for EF Core operators to work
            .ConfigureFiltersAndOrderByExpressionsForDbContextEntities<ApplicationDbContext>();
    }


Alternatively the :code:`OrderByExpressionDictionary<TSource>` class can be used,
either in the :code:`ApplyOrderByClientRequest` overload or when registering it as
a service in the dependency injection container.

