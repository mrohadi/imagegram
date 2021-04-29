using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> UpdateCommentAsync();
        Task<IEnumerable<Comment>> GetCommentsAsync(int postId);
        Task<Comment> GetCommentAsync(int commentId);
        Task<Comment> AddCommentAsync(int postId, int accountId, CommentDto dto);
    }
}