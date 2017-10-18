Getting Started
===============

In order to get started, you first have to add one or more packages from the following list to your project, via nuget:

* `FluentRestBuilder <https://www.nuget.org/packages/FluentRestBuilder>`_
* `FluentRestBuilder.EntityFrameworkCore <https://www.nuget.org/packages/FluentRestBuilder.EntityFrameworkCore>`_
* `FluentRestBuilder.Caching <https://www.nuget.org/packages/FluentRestBuilder.Caching>`_
* `FluentRestBuilder.HypertextApplicationLanguage <https://www.nuget.org/packages/FluentRestBuilder.HypertextApplicationLanguage>`_

Register FluentRestBuilder in your Startup:

.. sourcecode:: csharp

    public void ConfigureServices(IServiceCollection services)
    {
        ...

        services.AddMvc(options =>
        {
            // Add this if you want to use operators which depend
            // on the HttpContext.
            // Alternatively you can add this attribute to the controller
            // or the controller method where you want to use such an operator.
            options.Filters.Add(new HttpContextProviderAttribute());
        });

        services.AddFluentRestBuilder()
            // Optional. Only if you want to use EntityFramework operators.
            .AddEntityFrameworkCoreIntegration<ApplicationDbContext>()
            // Optional. Configures filters and order by expressions for pagination operators.
            .ConfigureFiltersAndOrderByExpressionsForDbContextEntities<ApplicationDbContext>();
    }

Now you can use FluentRestBuilder in your controllers. (See `sample <https://github.com/kyubisation/FluentRestBuilder/tree/dev/sample>`_ for an example.)

.. sourcecode:: csharp

    using System.Threading.Tasks;
    using FluentRestBuilder;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using ViewModels;

    [Route("posts")]
    public class PostController : ControllerBase
    {
        [HttpGet(Name = nameof(PostController))]
        public async Task<IActionResult> Get() =>
            await this.CreateQueryableSingle<Post>()
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedAt)
                .ApplyFilterByClientRequest()
                .ApplyOrderByClientRequest()
                .ApplyPaginationByClientRequest()
                .ToListAsync()
                .MapToRestCollection(p => new PostResponse(p, this.Url))
                .ToOkObjectResult();

        [HttpGet("{id}", Name = "PostResource")]
        public async Task<IActionResult> Get(int id) =>
            await this.CreateQueryableSingle<Post>()
                .Include(p => p.Author)
                .SingleOrDefaultAsync(p => p.Id == id)
                .NotFoundWhenNull()
                .Map(p => new PostResponse(p, this.Url))
                .ToOkObjectResult();

        ...
    }
