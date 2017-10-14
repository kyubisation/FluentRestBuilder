// <copyright file="CommentResponse.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using HypertextApplicationLanguage;
    using HypertextApplicationLanguage.Links;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class CommentResponse : RestEntity
    {
        public CommentResponse(Comment comment, IUrlHelper urlHelper)
        {
            this.Id = comment.Id;
            this.PostId = comment.PostId;
            this.AuthorId = comment.AuthorId;
            this.Title = comment.Title;
            this.Text = comment.Text;
            this.CreatedAt = comment.CreatedAt;
            this.Links = this.BuildLinks(comment, urlHelper);
            this.Embedded = this.BuildEmbeddedDictionary(comment, urlHelper);
        }

        public int Id { get; set; }

        public int PostId { get; set; }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        private IDictionary<string, object> BuildLinks(Comment comment, IUrlHelper urlHelper) =>
            new[]
            {
                new LinkToSelf(urlHelper.RouteUrl("CommentResource", new { id = comment.Id })),
                new NamedLink(
                    "post", urlHelper.RouteUrl("PostResource", new { id = comment.PostId })),
                new NamedLink(
                    "author", urlHelper.RouteUrl("UserResource", new { id = comment.AuthorId })),
            }.BuildLinks();

        private IDictionary<string, object> BuildEmbeddedDictionary(
            Comment comment, IUrlHelper urlHelper)
        {
            if (comment.Author == null && comment.Post == null)
            {
                return null;
            }

            var dictionary = new Dictionary<string, object>();
            if (comment.Author != null)
            {
                dictionary["author"] = new UserResponse(comment.Author, urlHelper);
            }

            if (comment.Post != null)
            {
                dictionary["post"] = new PostResponse(comment.Post, urlHelper);
            }

            return dictionary;
        }
    }
}
