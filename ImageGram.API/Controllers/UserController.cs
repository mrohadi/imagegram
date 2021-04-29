using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using ImageGram.API.Services;
using ImageGram.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Controllers
{
    [AuthenticationFilter]
    public class UserController : BaseApiController
    {
        private readonly IAccountRepository _repository;
        private readonly IHttpContextAccessor _http;
        public string token;
        public UserController(IAccountRepository repository, IHttpContextAccessor http)
        {
            _repository = repository;
            _http = http;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetUsersAsync()
        {
            var header = _http.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value.ToString();
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