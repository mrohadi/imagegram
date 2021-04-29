using System;

namespace ImageGram.Domain.DTOs
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}