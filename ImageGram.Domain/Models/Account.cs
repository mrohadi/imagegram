using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageGram.Domain.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}