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
        private readonly ICommentRepository _repository;
        private readonly IHttpContextAccessor _http;
        public CommentController(ICommentRepository repository, IHttpContextAccessor http)
        {
            _repository = repository;
            _http = http;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommnetsAsync([FromQuery]int postId)
        {
            var comments = await _repository.GetCommentsAsync(postId);

            return Ok(comments);
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<Comment>> GetSpecificComment(int commentId)
        {
            var comment = await _repository.GetCommentAsync(commentId);
            if(comment == null)
                return BadRequest("Comment not found!");
            return Ok(comment);
        }

        [HttpPost("{postId}")]
        public async Task<IActionResult> AddCommentAsync(int postId, [FromBody] CommentDto dto)
        {
            var tokenHeader = _http.HttpContext.Request.Headers
                .First(x => x.Key == "Authorization").Value.ToString();
            var userId = tokenHeader.GetUserId();

            var comment = await _repository.AddCommentAsync(postId, userId, dto);
            if(comment == null)
                return BadRequest("Failed to add comment!");
            return Ok(comment);
        }
    }
}