using System;
using System.Collections.Generic;
using ImageGram.API.Interfaces;
using ImageGram.Domain.Models;

namespace ImageGram.API.Services
{
    public class AuthenticationManagerService : IAuthenticationManagerService
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        {
            { "test1", "password1" },
            { "test2", "password2" }
        };
 
        private readonly IDictionary<string, string> tokens = new Dictionary<string, string>();
 
        public IDictionary<string, string> Tokens => tokens;
 
        public string Authenticate(Account account)
        {
            var guid = Guid.NewGuid().ToString();

            var token = $"X-Account-{account.AccountId}:{guid}";
 
            tokens.Add(token, account.Name);
 
            return token;
        }
    }
}