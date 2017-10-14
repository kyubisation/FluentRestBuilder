// <copyright file="HomeResponse.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.ViewModels
{
    using Controllers;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using Microsoft.AspNetCore.Mvc;

    public class HomeResponse : RestEntity
    {
        public HomeResponse(IUrlHelper urlHelper)
        {
            this.Links = new[]
            {
                new LinkToSelf(urlHelper.RouteUrl(nameof(DefaultController))),
                new NamedLink("posts", urlHelper.RouteUrl(nameof(PostController))),
                new NamedLink("comments", urlHelper.RouteUrl(nameof(CommentController))),
                new NamedLink("users", urlHelper.RouteUrl(nameof(UserController))),
            }.BuildLinks();
        }
    }
}
