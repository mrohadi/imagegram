using System.Threading.Tasks;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface ITokenManager
    {
        /// <summary>
        /// Authenticate the user when user exist in the data base
        /// </summary>
        /// <param name="dto">Account object to loggin</param>
        /// <returns>True when user exist in the data base</returns>
        Task<bool> Authenticate(AccountDto dto);
        /// <summary>
        /// Generate the token if the user loggin or register successfully
        /// </summary>
        /// <param name="account">Account object to generate the token</param>
        /// <returns>Token object that contain the token and expiry date</returns>
        TokenDto GenerateToken(Account account);
        /// <summary>
        /// Varify the authenticate user if the token same in list of token, and if the expity date is not expired
        /// </summary>
        /// <param name="token">Token to verify</param>
        /// <returns>True if all the conditions are met</returns>
        bool VerifyToken(string token);
    }
}