using System;
using System.Collections.Generic;

namespace ImageGram.Domain.DTOs
{
    public class CommentsToReturn
    {
        public IEnumerable<int> CommentId { get; set; }
        public List<string> Contents { get; set; }
        public List<DateTime> CreatedAts { get; set; }
        public int AccountId { get; set; }
    }
}