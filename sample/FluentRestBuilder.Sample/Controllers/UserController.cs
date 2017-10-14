// <copyright file="UserController.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using ViewModels;

    [Route("users")]
    public class UserController : ControllerBase
    {
        [HttpGet(Name = nameof(UserController))]
        public async Task<IActionResult> Get() =>
            await this.CreateQueryableSingle<User>()
                .OrderBy(u => u.Name)
                .ApplyFilterByClientRequest()
                .ApplyOrderByClientRequest()
                .ApplyPaginationByClientRequest()
                .ToListAsync()
                .MapToRestCollection(u => new UserResponse(u, this.Url))
                .ToOkObjectResult();

        [HttpGet("{id}", Name = "UserResource")]
        public async Task<IActionResult> Get(int id) =>
            await this.CreateEntitySingle<User>(id)
                .NotFoundWhenNull()
                .Map(u => new UserResponse(u, this.Url))
                .ToOkObjectResult();
    }
}
