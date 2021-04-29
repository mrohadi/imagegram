using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImageGram.API.Interfaces;
using ImageGram.Domain;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;

namespace ImageGram.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ImageGramContext _context;
        public AccountRepository(ImageGramContext context)
        {
            _context = context;
        }

        public async Task<Account> AddAccountAsync(AccountDto accountDto)
        {
            var newUser = new Account
            {
                Name = accountDto.Name
            };
            var user = await Task.FromResult(_context
                .Accounts
                .Add(newUser));
            return user.Entity;
        }

        public void DeleteAccountAsync(Account account)
        {
            _context.Accounts.Remove(account);
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _context
                .Accounts
                .Include(p => p.Posts)
                .Include(c => c.Comments)
                .SingleOrDefaultAsync(x => x.AccountId == accountId);
        }

        public async Task<Account> GetAccountByUsernameAsync(string name)
        {
            return await _context
                .Accounts
                .Include(p => p.Posts)
                .Include(c => c.Comments)
                .SingleOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync()
        {
            return await _context
                .Accounts
                .Include(p => p.Posts)
                .Include(c => c.Comments)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context
                .SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExists(string name)
        {
            return await _context
                .Accounts
                .AnyAsync(x => x.Name == name.ToLower());
        }
    }
}