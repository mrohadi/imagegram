using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using ImageGram.Domain;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ImageGram.API.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly List<TokenDto> listTokens;
        private readonly IServiceScopeFactory _serviceFactory;

        public TokenManager(IServiceScopeFactory serviceScopeFactory)
        {
            listTokens = new List<TokenDto>();
            _serviceFactory = serviceScopeFactory;
        }

        public async Task<bool> Authenticate(AccountDto dto)
        {
            using var scope = _serviceFactory.CreateScope();
            var serviecScoped = scope.ServiceProvider.GetService<IUnitOfWork>();
            var user = await serviecScoped.AccountRepository
                .GetAccountByUsernameAsync(dto.Name);

            if (user == null)
                return false;

            if (!string.IsNullOrEmpty(dto.Name) &&
                dto.Name != user.Name)
                return false;

            return true;
        }

        public TokenDto GenerateToken(Account account)
        {
            var guid = Guid.NewGuid().ToString();
            var result = $"X-Account-{account.AccountId}:{guid}";
            var token = new TokenDto
            {
                Token = result,
                ExpiryDate = DateTime.Now.AddMinutes(5)
            };

            listTokens.Add(token);
            return token;
        }

        public bool VerifyToken(string token)
        {
            if (!listTokens.Any(x => x.Token == token && x.ExpiryDate > DateTime.Now))
                return false;
                
            return true;
        }
    }
}