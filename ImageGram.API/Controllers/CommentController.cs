using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGram.API.Helper;
using ImageGram.API.Interfaces;
using ImageGram.API.Services;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageGram.API.Controllers
{
    [AuthenticationFilter]
    public class CommentController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _http;
        public CommentController(IUnitOfWork unitOfWork, IHttpContextAccessor http)
        {
            _unitOfWork = unitOfWork;
            _http = http;
        }

        /// <summary>
        /// Controller to get all of lists comments in the data base based on postid
        /// </summary>
        /// <param name="postId">Unique postid</param>
        /// <returns>Action result of lists all of comments</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommnetsAsync([FromQuery]int postId)
        {
            var comments = await _unitOfWork.CommentRepository.GetCommentsAsync(postId);

            return Ok(comments);
        }

        /// <summary>
        /// Controller to get specific comment based on commnetid
        /// </summary>
        /// <param name="commentId">Unique commnetid</param>
        /// <returns>Specific comment</returns>
        [HttpGet("{commentId}")]
        public async Task<ActionResult<Comment>> GetSpecificComment(int commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetCommentAsync(commentId);
            if(comment == null)
                return BadRequest("Comment not found!");

            return Ok(comment);
        }

        /// <summary>
        /// Controller to add new comment on specific post based on postid
        /// </summary>
        /// <param name="postId">Unique postid</param>
        /// <param name="dto">Comment object to post</param>
        /// <returns>Action result of comment</returns>
        [HttpPost("{postId}")]
        public async Task<IActionResult> AddCommentAsync(int postId, [FromBody] CommentDto dto)
        {
            var tokenHeader = _http.HttpContext.Request.Headers
                .First(x => x.Key == "Authorization").Value.ToString();
            var userId = tokenHeader.GetUserId();

            var comment = await _unitOfWork.CommentRepository.AddCommentAsync(postId, userId, dto);
            if(comment == null)
                return BadRequest("Failed to add comment!");
            
            if(!await _unitOfWork.Complete())
                return BadRequest("Failed to save comment!");
                
            return Ok(comment);
        }
    }
}