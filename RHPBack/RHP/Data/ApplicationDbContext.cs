using Microsoft.EntityFrameworkCore;
using RHP.Models;


namespace RHP.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        
        //Models
        public DbSet<Models.Dice> Users { get; set; }
        public DbSet<Models.Roll> Roll { get; set; }
        public DbSet<Models.Player> Player { get; set; }
        public DbSet<Models.Hall> Hall { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }

}
