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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(
            IUnitOfWork unitOfWork,
            ITokenManager tokenManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Controller to register new user
        /// </summary>
        /// <param name="accountDto">Account object to register new user</param>
        /// <returns>Action result of token object</returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AccountDto accountDto) 
        {
            if(string.IsNullOrEmpty(accountDto.Name))
                return BadRequest("Please privide name");

            if(await _unitOfWork.AccountRepository.UserExists(accountDto.Name))
                return BadRequest("User already exists!");

            var newUser = await _unitOfWork.AccountRepository.AddAccountAsync(accountDto);

            if(!await _unitOfWork.Complete())
                return BadRequest("Failed to save new user!");
            
            if(!await _tokenManager.Authenticate(accountDto))
                return Unauthorized();

            return Ok(_tokenManager.GenerateToken(newUser));
        }
        
        /// <summary>
        /// Controller to loggin the user
        /// </summary>
        /// <param name="accountDto">Account object to loggin the user</param>
        /// <returns>Action result of token object</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AccountDto accountDto)
        {
            var user = await _unitOfWork.AccountRepository.GetAccountByUsernameAsync(accountDto.Name);

            if(user == null)
                return Unauthorized("Login Failed, please check your name!");
            
            if(!await _tokenManager.Authenticate(accountDto))
                return Unauthorized();

            return Ok(_tokenManager.GenerateToken(user));
        }

        /// <summary>
        /// Delete the currently logged in user
        /// </summary>
        /// <returns>Action result of the conditions</returns>
        [AuthenticationFilter]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteAccountAsync()
        {
            var requestToken = _httpContextAccessor.HttpContext.Request.Headers
                .First(x => x.Key == "Authorization").Value.ToString();
            var accountId = requestToken.GetUserId();

            var user = await _unitOfWork.AccountRepository.GetAccountByIdAsync(accountId);
            
            _unitOfWork.AccountRepository.DeleteAccountAsync(user);

            if(!await _unitOfWork.Complete())
                return BadRequest("Failed to delete account!");
                
            return Ok("User deleted successfully!");
        }
    }
}