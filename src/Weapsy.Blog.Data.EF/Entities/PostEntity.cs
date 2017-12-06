﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Weapsy.Blog.Domain.Posts;

namespace Weapsy.Blog.Data.EF.Entities
{
    public class PostEntity
    {
        public Guid Id { get; set; }
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        [NotMapped]
        public IEnumerable<string> Tags { get; set; }
        public PostStatus Status { get; set; }
        public DateTime StatusTimeStamp { get; set; }
    }
}