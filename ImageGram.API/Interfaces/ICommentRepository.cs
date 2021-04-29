using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Get all of comments in the data base
        /// </summary>
        /// <param name="postId">Unique postid</param>
        /// <returns>List of comments</returns>
        Task<IEnumerable<Comment>> GetCommentsAsync(int postId);
        /// <summary>
        /// Get specific comment in the data base based on commentid
        /// </summary>
        /// <param name="commentId">Unique commentid</param>
        /// <returns>Specific comment</returns>
        Task<Comment> GetCommentAsync(int commentId);
        /// <summary>
        /// Add new comment to the data base on specific post
        /// </summary>
        /// <param name="postId">Unique postid that determine the post to comment</param>
        /// <param name="accountId">Unique accountid that determine currently logged in user</param>
        /// <param name="dto">Comment object to save</param>
        /// <returns>Created comment</returns>
        Task<Comment> AddCommentAsync(int postId, int accountId, CommentDto dto);
    }
}