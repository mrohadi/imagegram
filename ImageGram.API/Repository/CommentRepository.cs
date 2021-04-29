using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using ImageGram.Domain;
using ImageGram.Domain.DTOs;
using ImageGram.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.API.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ImageGramContext _context;
        public CommentRepository(ImageGramContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddCommentAsync(int postId, int accountId, CommentDto dto)
        {
            var post = await _context.Posts
                .SingleOrDefaultAsync(x => x.PostId == postId);

            var comment = new Comment
            {
                Content = dto.Content,
                CreateAt = dto.CreatedAt,
                PostId = post.PostId,
                AccountId = accountId
            };

            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            var comment = await _context.Comments
                .SingleOrDefaultAsync(c => c.CommentId == commentId);
            
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync(int postId)
        {   
            var comments = await _context.Posts
                .Where(x => x.PostId == postId)
                .SelectMany(x => x.Comments)
                .OrderByDescending(date => date.CreateAt)
                .ToListAsync();

            return comments;
        }

        public Task<Comment> UpdateCommentAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}