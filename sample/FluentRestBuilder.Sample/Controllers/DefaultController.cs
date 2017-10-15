// <copyright file="DefaultController.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    [Route("")]
    public class DefaultController : ControllerBase
    {
        [HttpGet(Name = nameof(DefaultController))]
        public async Task<IActionResult> Home() =>
            await this.CreateSingle(new HomeResponse(this.Url))
                .CacheInMemoryCache(nameof(DefaultController))
                .ToOkObjectResult();
    }
}
