// <copyright file="CommentRequest.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CommentRequest
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
