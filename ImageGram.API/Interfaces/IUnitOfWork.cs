using System.Threading.Tasks;

namespace ImageGram.API.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Account repository that contain all of queries of account object
        /// </summary>
        /// <value></value>
        IAccountRepository AccountRepository { get; }
        /// <summary>
        /// Post repository that contains all of quesries of post object
        /// </summary>
        /// <value></value>
        IPostRepository PostRepository { get; }
        /// <summary>
        /// Comment repository that contains all of queries of comment object
        /// </summary>
        /// <value></value>
        ICommentRepository CommentRepository { get; }
        /// <summary>
        /// Save all of queries
        /// </summary>
        /// <returns></returns>
        Task<bool> Complete();
    }
}