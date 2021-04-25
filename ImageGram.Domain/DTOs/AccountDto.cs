using System.ComponentModel.DataAnnotations;

namespace ImageGram.Domain.DTOs
{
    public class AccountDto
    {
        [Required]
        public string Name { get; set; }
    }
}