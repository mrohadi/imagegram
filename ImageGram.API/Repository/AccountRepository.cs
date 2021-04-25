using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using ImageGram.Domain;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ImageGramContext _context;
        public AccountRepository(ImageGramContext context)
        {
            _context = context;
        }

        public async Task<Account> AddAccountAsync(Account account)
        {
            var user = await Task.FromResult(_context.Accounts.Add(account));
            return user.Entity;
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.AccountId == accountId);
        }

        public async Task<Account> GetAccountByUsernameAsync(string name)
        {
            return await _context.Accounts.SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExists(string name)
        {
            return await _context.Accounts.AnyAsync(x => x.Name == name.ToLower());
        }
    }
}