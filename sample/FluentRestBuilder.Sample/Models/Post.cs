// <copyright file="Post.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
