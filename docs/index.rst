FluentRestBuilder Documentation
===============================

`FluentRestBuilder <https://github.com/kyubisation/FluentRestBuilder>`_ is a library to easily
build RESTful APIs on top of `MVC Core <https://github.com/aspnet/Mvc>`_. 

It is based on the IObservable<T> and IObserver<T> interfaces and inspired by the Reactive Extensions (Rx.NET).
It is however not fully compatible with Rx.NET, as FluentRestBuilder extends the IObservable<T> interface.

The motivation for this library is to reduce boilerplate code where possible, so instead of writing this:

.. sourcecode:: csharp

    [Route("posts")]
    public class PostController : ControllerBase
    {
        [HttpGet("{id}", Name = "PostResource")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await this.dbContext.Posts
                .Include(p => p.Author)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return this.NotFound();
            }

            var result = new PostResponse(post, this.Url);
            return this.Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromBody] PostRequest request, int id)
        {
            var post = await this.dbContext.Posts
                .Include(p => p.Author)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            post.Title = request.Title;
            post.Content = request.Content;
            await this.dbContext.SaveChangesAsync();

            var result = new PostResponse(post, this.Url);
            return this.Ok(post);
        }
    }

you can simply write the following:

.. sourcecode:: csharp

    [Route("posts")]
    public class PostController : ControllerBase
    {
        [HttpGet("{id}", Name = "PostResource")]
        public async Task<IActionResult> Get(int id) =>
            await this.CreateQueryableSingle<Post>()
                .Include(p => p.Author)
                .SingleOrDefaultAsync(p => p.Id == id)
                .NotFoundWhenNull()
                .Map(p => new PostResponse(p, this.Url))
                .ToOkObjectResult();

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromBody] PostRequest request, int id) =>
            await this.CreateEntitySingle<Post>(id)
                .NotFoundWhenNull()
                .BadRequestWhenModelStateIsInvalid(this.ModelState)
                .Do(p =>
                {
                    p.Title = request.Title;
                    p.Content = request.Content;
                })
                .SaveChangesAsync()
                .Map(p => new PostResponse(p, this.Url))
                .ToOkObjectResult();
    }


.. toctree::
   :maxdepth: 2
   :caption: Contents:

   getting-started/index.rst
   observables/index.rst
   operators/index.rst
   pagination/index.rst
