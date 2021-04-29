using System.Threading.Tasks;
using ImageGram.API.Interfaces;
using ImageGram.Domain;

namespace ImageGram.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ImageGramContext _context;
        public UnitOfWork(ImageGramContext context)
        {
            _context = context;
        }

        public IAccountRepository AccountRepository => new AccountRepository(_context);

        public IPostRepository PostRepository => new PostRepository(_context);

        public ICommentRepository CommentRepository => new CommentRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}