using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Delete currently logged in user from the data base
        /// </summary>
        /// <param name="accountId">Unique accountid</param>
        /// <returns>No return</returns>
        void DeleteAccountAsync(Account account);
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
        /// Get specific user by name
        /// </summary>
        /// <param name="username">Unique name</param>
        /// <returns>User object</returns>
        Task<Account> GetAccountByUsernameAsync(string name);
        /// <summary>
        /// Add new user to the data base
        /// </summary>
        /// <param name="account">Account object to save to the data base</param>
        /// <returns>Newly created user</returns>
        Task<Account> AddAccountAsync(AccountDto accountDto);
        /// <summary>
        /// Check if the user already exist in the data base
        /// </summary>
        /// <param name="username">Name to check</param>
        /// <returns>Return true if the user already exist</returns>
        Task<bool> UserExists(string name);
    }
}