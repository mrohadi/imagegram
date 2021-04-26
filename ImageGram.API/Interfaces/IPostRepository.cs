using System.Collections.Generic;
using System.Threading.Tasks;
using ImageGram.Domain.Models;

namespace ImageGram.API.Interfaces
{
    public interface IPostRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task Delete(int postId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Post> AddPostAsync(string imageUrl, int accountId);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetPostsAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<Post> GetPostByIdAsync(int postId);
    }
}