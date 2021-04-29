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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageHandler _imageHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostController(
            IUnitOfWork unitOfWork,
            IImageHandler imageHandler,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _imageHandler = imageHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Controller to get lists of all posts in the data base
        /// </summary>
        /// <returns>Lists of posts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _unitOfWork.PostRepository.GetPostsAsync();
            if(posts == null)
                return BadRequest("Posts Not Found!");

            return Ok(posts);
        }
        
        /// <summary>
        /// Controller to get specific post in the data base
        /// </summary>
        /// <param name="postId">Unique postid</param>
        /// <returns>Specific post</returns>
        [HttpGet("{postId}")]
        public async Task<ActionResult> GetPostAsync(int postId)
        {
            var post = await _unitOfWork.PostRepository.GetPostByIdAsync(postId);
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

        /// <summary>
        /// Controller to add new post into the data base
        /// </summary>
        /// <param name="file">Image file to add</param>
        /// <returns>Action resut based on all of the conditions</returns>
        [HttpPost]
        public async Task<IActionResult> AddPostAsync(IFormFile file)
        {   
            var tokenHeader = _httpContextAccessor.HttpContext.Request.Headers
                .First(x => x.Key == "Authorization").Value.ToString();
            
            var accountId = tokenHeader.GetUserId();

            var imageUrl = await _imageHandler.UploadImage(file);
            if(imageUrl == "Invalid image file")
                return BadRequest("Invalid image file");

            var user = await _unitOfWork.AccountRepository.GetAccountByIdAsync(accountId);
            if(user == null)
                return BadRequest("User Not Found!");

            await _unitOfWork.PostRepository.AddPostAsync(imageUrl.ToString(), accountId); 
            
            if(await _unitOfWork.Complete())
                return Ok("Post Image Successfully!");

            return BadRequest("Failed to Post Photo!");
        }
    }
}