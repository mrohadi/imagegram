using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ImageGram.API.Services
{
    public class AuthenticationOptions : AuthenticationSchemeOptions
    { }

    public class AuthenticationHandlerService : AuthenticationHandler<AuthenticationOptions>
    {
        private readonly IAuthenticationManagerService _authManager;
 
        public AuthenticationHandlerService(
            IOptionsMonitor<AuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IAuthenticationManagerService authManager) 
            : base(options, logger, encoder, clock)
        {
            _authManager = authManager;
        }
 
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return await Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
 
            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
                return await Task.FromResult(AuthenticateResult.NoResult());
 
            if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                return await  Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
 
            string token = authorizationHeader["Bearer".Length..].Trim();
 
            if (string.IsNullOrEmpty(token))
                return await Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
 
            try
            {
                return await ValidateToken(token);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(AuthenticateResult.Fail(ex.Message));
            }
        }
 
        private async Task<AuthenticateResult> ValidateToken(string token)
        {
            var validatedToken = _authManager.Tokens.FirstOrDefault(t => t.Key == token);
            if (validatedToken.Key == null)
                return await Task.FromResult(AuthenticateResult.Fail("Unauthorized"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedToken.Value),
            };
 
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}