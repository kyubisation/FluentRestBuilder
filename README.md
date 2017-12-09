FluentRestBuilder
===

AppVeyor: [![AppVeyor](https://ci.appveyor.com/api/projects/status/ubv5td4t0xtmql6h?svg=true)](https://ci.appveyor.com/project/kyubisation/fluentrestbuilder)
Travis:   [![Travis](https://travis-ci.org/kyubisation/FluentRestBuilder.svg?branch=dev)](https://travis-ci.org/kyubisation/FluentRestBuilder)

[Documentation](http://fluentrestbuilder.readthedocs.io/)

FluentRestBuilder aims in helping you build REST APIs on top of ASP.NET Core MVC.


The motivation for this library is to reduce boilerplate code where possible, so instead of writing this:

```csharp

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
	
```

you can simply write the following:

```csharp

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
	
```
