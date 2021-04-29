using System.Linq;
using System.Threading.Tasks;
using ImageGram.API.Helper;
using ImageGram.API.Interfaces;
using ImageGram.API.Services;
using ImageGram.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountRepository _repository;
        private readonly ITokenManager _tokenManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            IAccountRepository repository, 
            ITokenManager tokenManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _tokenManager = tokenManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AccountDto accountDto) 
        {
            if(string.IsNullOrEmpty(accountDto.Name))
                return BadRequest("Please privide name");

            if(await _repository.UserExists(accountDto.Name))
                return BadRequest("User already exists!");

            var newUser = await _repository.AddAccountAsync(accountDto);

            if(!await _repository.SaveChangesAsync())
                return BadRequest("Failed to save new user!");
            
            if(!await _tokenManager.Authenticate(accountDto))
                return Unauthorized();
            return Ok(_tokenManager.GenerateToken(newUser));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AccountDto accountDto)
        {
            var user = await _repository.GetAccountByUsernameAsync(accountDto.Name);

            if(user == null)
                return Unauthorized("Login Failed, please check your name!");
            
            if(!await _tokenManager.Authenticate(accountDto))
                return Unauthorized();
            return Ok(_tokenManager.GenerateToken(user));
        }

        [AuthenticationFilter]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAccountAsync()
        {
            var requestToken = _httpContextAccessor.HttpContext.Request.Headers
                .First(x => x.Key == "Authorization").Value.ToString();
            var accountId = requestToken.GetUserId();

            var user = await _repository.GetAccountByIdAsync(accountId);
            
            _repository.DeleteAccountAsync(user);

            if(!await _repository.SaveChangesAsync())
                return BadRequest("Failed to delete account!");
            return Ok("User deleted successfully!");
        }
    }
}