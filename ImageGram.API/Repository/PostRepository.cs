using System;
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
    public class PostRepository : IPostRepository
    {
        private readonly ImageGramContext _context;
        public PostRepository(ImageGramContext context)
        {
            _context = context;
        }

        public async Task<Post> AddPostAsync(string imageUrl, int accountId)
        {
            var post = new Post
            {
                ImageUrl = imageUrl,
                AccountId = accountId,
                CreatedAt = DateTime.Now,
                Comments = new List<Comment> { }
            };

            await _context.Posts.AddAsync(post);

            return post;
        }

        public async Task Delete(int postId)
        {
            var post = await _context.Posts
                .Include(c => c.Comments)
                .SingleOrDefaultAsync(x => x.PostId == postId);
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _context.Posts
                .Where(x => x.PostId == postId)
                .Include(c => c.Comments)
                .OrderByDescending(x => x.Comments.Count)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _context.Posts
                .Include(c => c.Comments)
                .OrderByDescending(x => x.Comments.Count)
                .ToListAsync();
        }
    }
}