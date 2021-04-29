using System;
using System.Collections.Generic;

namespace ImageGram.Domain.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AccountId { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}