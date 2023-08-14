using ALEXforums.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ALEXforums.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ForumPost> ForumPosts { get; set; }
        public DbSet<ForumPostComment> ForumPostComments { get; set; }
        public DbSet<ForumCategory> ForumCategories { get; set; }



        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ALEXforums");
            modelBuilder.HasCharSet(null, DelegationModes.ApplyToDatabases);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.ForumPosts)
                      .WithOne(p => p.User)
                      .HasForeignKey(p => p.UserId);

                entity.HasMany(u => u.ForumPostComments)
                      .WithOne(c => c.User)
                      .HasForeignKey(c => c.UserId);
            });

            modelBuilder.Entity<ForumPost>(entity =>
            {
                entity.HasOne(p => p.User)
                      .WithMany(u => u.ForumPosts)
                      .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.ForumPosts)
                      .HasForeignKey(p => p.CategoryId);

                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.PostId);
            });

            modelBuilder.Entity<ForumPostComment>(entity =>
            {
                entity.HasOne(c => c.User)
                      .WithMany(u => u.ForumPostComments)
                      .HasForeignKey(c => c.UserId);

                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId);
            });

            modelBuilder.Entity<ForumCategory>(entity =>
            {
                entity.HasMany(c => c.ForumPosts)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId);
            });
        }
    }
}
