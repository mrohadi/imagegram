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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _http;
        public string token;
        public UserController(IUnitOfWork unitOfWork, IHttpContextAccessor http)
        {
            _unitOfWork = unitOfWork;
            _http = http;
        }

        /// <summary>
        /// Controller to get lists of all users in the data base
        /// </summary>
        /// <returns>List of users</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetUsersAsync()
        {
            var header = _http.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value.ToString();
            var user = await _unitOfWork.AccountRepository.GetAccountsAsync();
            if(user == null)
                return BadRequest("User Not Found!");

            return Ok(user);                
        }

        /// <summary>
        /// Controller to get specific user based on their accountid
        /// </summary>
        /// <param name="accountId">Unique accountid</param>
        /// <returns>Specific user</returns>
        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetUserAsync(int accountId)
        {
            var user = await _unitOfWork.AccountRepository.GetAccountByIdAsync(accountId);
            if(user == null)
                return BadRequest("User not found!");
            
            return Ok(user);
        }  
    }
}