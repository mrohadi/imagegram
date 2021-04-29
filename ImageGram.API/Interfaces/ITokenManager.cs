using System.Threading.Tasks;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface ITokenManager
    {
        Task<bool> Authenticate(AccountDto dto);
        TokenDto GenerateToken(Account account);
        bool VerifyToken(string token);
    }
}