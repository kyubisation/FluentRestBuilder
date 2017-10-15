// <copyright file="PostController.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.Controllers
{
    using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostRequest request) =>
            await this.CreateSingle(request)
                .BadRequestWhenModelStateIsInvalid(this.ModelState)
                .Map(r => new Post
                {
                    AuthorId = this.User.GetUserId(),
                    Title = r.Title,
                    Content = r.Content,
                })
                .InsertEntity()
                .ToCreatedAtRouteResult("PostResource", e => new { id = e.Id });

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
                .ToOkObjectResult();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await this.CreateEntitySingle<Post>(id)
                .NotFoundWhenNull()
                .ForbiddenWhen(p => p.AuthorId != this.User.GetUserId())
                .DeleteEntity()
                .ToNoContentResult();
    }
}
