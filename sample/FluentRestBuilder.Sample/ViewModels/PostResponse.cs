// <copyright file="PostResponse.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Controllers;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class PostResponse : RestEntity
    {
        public PostResponse(Post post, IUrlHelper urlHelper)
        {
            this.Id = post.Id;
            this.AuthorId = post.AuthorId;
            this.Title = post.Title;
            this.Content = post.Content;
            this.CreatedAt = post.CreatedAt;
            this.Links = this.BuildLinks(post, urlHelper);
            this.Embedded = this.BuildEmbeddedDictionary(post, urlHelper);
        }

        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        private IDictionary<string, object> BuildLinks(Post post, IUrlHelper urlHelper) =>
            new[]
            {
                new LinkToSelf(urlHelper.RouteUrl("PostResource", new { id = post.Id })),
                new NamedLink(
                    "comments",
                    urlHelper.RouteUrl(nameof(PostCommentController), new { postId = post.Id })),
                new NamedLink(
                    "author", urlHelper.RouteUrl("UserResource", new { id = post.AuthorId })),
            }.BuildLinks();

        private IDictionary<string, object> BuildEmbeddedDictionary(Post post, IUrlHelper urlHelper)
        {
            if (post.Author == null)
            {
                return null;
            }

            return new Dictionary<string, object>
            {
                ["author"] = new UserResponse(post.Author, urlHelper),
            };
        }
    }
}
