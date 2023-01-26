using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(m => m.Messages)
                .HasForeignKey(m => m.UserId);
        }
    }
}