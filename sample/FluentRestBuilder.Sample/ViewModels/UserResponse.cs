// <copyright file="UserResponse.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.ViewModels
{
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class UserResponse : RestEntity
    {
        public UserResponse(User user, IUrlHelper urlHelper)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Links = new[]
            {
                new LinkToSelf(urlHelper.RouteUrl("UserResource", new { id = user.Id })),
            }.BuildLinks();
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
