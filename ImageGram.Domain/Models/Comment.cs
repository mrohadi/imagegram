using System;
using System.ComponentModel.DataAnnotations;

namespace ImageGram.Domain.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }

        public int AccountId { get; set; }

        public int PostId { get; set; }
    }
}