using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGram.API.Helper;
using ImageGram.API.Interfaces;
using ImageGram.API.Services;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Controllers
{
    [AuthenticationFilter]
    public class PostController : BaseApiController
    {
        private readonly IImageHandler _imageHandler;
        private readonly IPostRepository _postRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostController(
            IImageHandler imageHandler,
            IPostRepository postRepo,
            IAccountRepository accountRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _imageHandler = imageHandler;
            _postRepo = postRepo;
            _accountRepo = accountRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _postRepo.GetPostsAsync();
            if(posts == null)
                return BadRequest("Posts Not Found!");

            return Ok(posts);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("{postId}")]
        public async Task<ActionResult> GetPostAsync(int postId)
        {
            var post = await _postRepo.GetPostByIdAsync(postId);
            if(post == null)
                return BadRequest("Post Not Found!");
            
            var postToReturn = new PostDto
            {
                ImageUrl = post.ImageUrl,
                CreatedAt = post.CreatedAt,
                Comments = post.Comments
            };
            
            return Ok(postToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddPostAsync(IFormFile file)
        {   
            var tokenHeader = _httpContextAccessor.HttpContext.Request.Headers
                .First(x => x.Key == "Authorization").Value.ToString();
            
            var accountId = tokenHeader.GetUserId();

            var imageUrl = await _imageHandler.UploadImage(file);
            if(imageUrl == null)
                return BadRequest("Failed to Post Photo!");

            var user = await _accountRepo.GetAccountByIdAsync(accountId);
            if(user == null)
                return BadRequest("User Not Found!");

            await _postRepo.AddPostAsync(imageUrl, accountId); 
            if(await _postRepo.SaveChangesAsync())
                return Ok("Post Image Successfully!");
            return BadRequest("Failed to Post Photo!");
        }
    }
}