// <copyright file="CommentController.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using ViewModels;

    [Route("comments")]
    public class CommentController : ControllerBase
    {
        [HttpGet(Name = nameof(CommentController))]
        public async Task<IActionResult> Get() =>
            await this.CreateQueryableSingle<Comment>()
                .Include(c => c.Author)
                .OrderByDescending(c => c.CreatedAt)
                .ApplyFilterByClientRequest()
                .ApplyOrderByClientRequest()
                .ApplyPaginationByClientRequest()
                .ToListAsync()
                .MapToRestCollection(c => new CommentResponse(c, this.Url))
                .ToOkObjectResult();

        [HttpGet("{id}", Name = "CommentResource")]
        public async Task<IActionResult> Get(int id) =>
            await this.CreateQueryableSingle<Comment>()
                .Include(c => c.Author)
                .SingleOrDefaultAsync(c => c.Id == id)
                .NotFoundWhenNull()
                .Map(p => new CommentResponse(p, this.Url))
                .ToOkObjectResult();

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromBody] CommentRequest request, int id) =>
            await this.CreateEntitySingle<Comment>(id)
                .NotFoundWhenNull()
                .BadRequestWhen(() => !this.ModelState.IsValid, this.ModelState)
                .Do(p =>
                {
                    p.Title = request.Title;
                    p.Text = request.Text;
                })
                .SaveChangesAsync()
                .ToOkObjectResult();

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) =>
            await this.CreateEntitySingle<Comment>(id)
                .NotFoundWhenNull()
                .ForbiddenWhen(c => c.AuthorId != this.User.GetUserId())
                .DeleteEntity()
                .ToNoContentResult();
    }
}
