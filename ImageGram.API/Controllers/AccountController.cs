using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _repository;
        private readonly IAuthenticationManagerService _authManager;
        public AccountController(IAccountRepository repository, IAuthenticationManagerService authManager)
        {
            _repository = repository;
            _authManager = authManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserTokenDto>> RegisterAsync([FromBody] Account account) 
        {
            var user = await _repository.AddAccountAsync(account);
            if(!await _repository.SaveChangesAsync())
                return BadRequest("Failed to register a user!");

            if(user.Name == null)
                return BadRequest("Please provide name");
            
            var token = _authManager.Authenticate(user);
            
            if(token == null)
                return Unauthorized("Unauthorized user, please login or register!");
            
            return new UserTokenDto
            {
                Name = account.Name,
                Token = token
            };
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserTokenDto>> LoginAsync([FromBody] Account account)
        {
            var user = await _repository.GetAccountByUsernameAsync(account.Name);

            if(user == null)
                return Unauthorized("Login Failed, please check your name!");
            
            var token = _authManager.Authenticate(user);

            if(token == null)
                return Unauthorized("Unauthotize user, please login or register!");
            
            return new UserTokenDto
            {
                Name = user.Name,
                Token = token
            };
        }
    }
}