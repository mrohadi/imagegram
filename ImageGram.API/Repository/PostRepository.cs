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
        private readonly ImageGramContext _contextFactory;
        public PostRepository(ImageGramContext contextFactory)
        {
            _contextFactory = contextFactory;
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

            await _contextFactory.Posts.AddAsync(post);

            return post;
        }

        public async Task Delete(int postId)
        {
            var post = await _contextFactory.Posts
                .Include(c => c.Comments)
                .SingleOrDefaultAsync(x => x.PostId == postId);
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _contextFactory.Posts
                .Where(x => x.PostId == postId)
                .Include(c => c.Comments)
                .OrderByDescending(x => x.Comments.Count)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            return await _contextFactory.Posts
                .Include(c => c.Comments)
                .OrderByDescending(x => x.Comments.Count)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _contextFactory.SaveChangesAsync() > 0;
        }
    }
}