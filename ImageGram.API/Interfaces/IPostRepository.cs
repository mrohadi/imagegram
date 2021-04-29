using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface IPostRepository
    {
        /// <summary>
        /// Delete the post by postid
        /// </summary>
        /// <param name="postId">Unique postid</param>
        /// <returns>No return</returns>
        Task Delete(int postId);
        /// <summary>
        /// Add post to the data base
        /// </summary>
        /// <returns>Added post</returns>
        Task<Post> AddPostAsync(string imageUrl, int accountId);
        /// <summary>
        /// Get all of lists post in the data base
        /// </summary>
        /// <returns>List of posts</returns>
        Task<IEnumerable<Post>> GetPostsAsync();
        /// <summary>
        /// Get specific post in the data base
        /// </summary>
        /// <param name="postId">Uniques postid</param>
        /// <returns>Specific post</returns>
        Task<Post> GetPostByIdAsync(int postId);
    }
}