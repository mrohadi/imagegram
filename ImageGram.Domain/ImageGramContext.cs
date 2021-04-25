using ImageGram.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageGram.Domain
{
    public class ImageGramContext : DbContext
    {
        public ImageGramContext(DbContextOptions<ImageGramContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasMany(p => p.Posts)
                .WithOne(a => a.Account);
            
            modelBuilder.Entity<Account>()
                .HasMany(c => c.Comments)
                .WithOne(a => a.Account);

            modelBuilder.Entity<Post>()
                .HasOne(a => a.Account)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.AccountId)
                .IsRequired();
            
            modelBuilder.Entity<Comment>()
                .HasOne(a => a.Account)
                .WithMany(c => c.Comments)
                .HasForeignKey(a => a.AccountId)
                .IsRequired();
            
            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.PostId);
        }
    }
}