using System;
using System.Collections.Generic;
using ImageGram.Domain.Models;

namespace ImageGram.Domain.DTOs
{
    public class PostDto
    {
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}