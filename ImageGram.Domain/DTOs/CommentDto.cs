using System;

namespace ImageGram.Domain.DTOs
{
    public class CommentDto
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}