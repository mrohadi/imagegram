using ImageGram.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.Domain
{
    public class ImageGramContext : DbContext
    {
        public ImageGramContext(DbContextOptions<ImageGramContext> options) 
            : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}