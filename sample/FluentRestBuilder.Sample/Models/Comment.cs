// <copyright file="Comment.cs" company="Kyubisation">
// Copyright (c) Kyubisation. All rights reserved.
// </copyright>

namespace FluentRestBuilder.Sample.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Text { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
