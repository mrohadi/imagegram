using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using ImageGram.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IAccountRepository _repository;
        public UserController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetUsersAsync()
        {
            var user = await _repository.GetAccountsAsync();
            if(user == null)
                return BadRequest("User Not Found!");

            return Ok(user);                
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetUserAsync(int accountId)
        {
            var user = await _repository.GetAccountByIdAsync(accountId);
            if(user == null)
                return BadRequest("User not found!");
            
            return Ok(user);
        }
    }
}