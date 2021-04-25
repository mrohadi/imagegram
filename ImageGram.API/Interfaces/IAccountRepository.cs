using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> SaveChangesAsync();
        /// <summary>
        /// Get lists of all users from account table
        /// </summary>
        /// <returns>List of users</returns>
        Task<IEnumerable<Account>> GetAccountsAsync();
        /// <summary>
        /// Get specific user from account table
        /// </summary>
        /// <param name="accountId">Unique userId</param>
        /// <returns>specific user</returns>
        Task<Account> GetAccountByIdAsync(int accountId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<Account> GetAccountByUsernameAsync(string name);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<Account> AddAccountAsync(Account account);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> UserExists(string name);
    }
}