// <copyright file="PostCommentController.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using ViewModels;

    [Route("posts/{postId}/comments")]
    public class PostCommentController : ControllerBase
    {
        [HttpGet(Name = nameof(PostCommentController))]
        public async Task<IActionResult> Get(int postId) =>
            await this.CreateQueryableSingle<Comment>()
                .Include(c => c.Author)
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ApplyFilterByClientRequest()
                .ApplyOrderByClientRequest()
                .ApplyPaginationByClientRequest()
                .ToListAsync()
                .MapToRestCollection(c => new CommentResponse(c, this.Url))
                .ToOkObjectResult();

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentRequest request, int postId) =>
            await this.CreateSingle(request)
                .BadRequestWhenModelStateIsInvalid(this.ModelState)
                .Map(r => new Comment
                {
                    AuthorId = this.User.GetUserId(),
                    PostId = postId,
                    Title = r.Title,
                    Text = r.Text,
                })
                .InsertEntity()
                .ToCreatedAtRouteResult("CommentResource", e => new { id = e.Id });
    }
}
