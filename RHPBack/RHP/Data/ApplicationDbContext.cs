using Microsoft.EntityFrameworkCore;
using RHP.Entities.Models;

namespace RHP.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public ApplicationDbContext()
        {}

        //Models
        public DbSet<User> User { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Hall> Hall { get; set; }
        public DbSet<Roll> Roll { get; set; }
        public DbSet<Dice> Dice { get; set; }
        public DbSet<ActionLog> ActionLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.name)
                .IsUnique();

            modelBuilder.Entity<Player>()
                .HasOne(p => p.user)
                .WithOne(u => u.player)
                .HasForeignKey<Player>(p => p.userId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
             
            modelBuilder.Entity<Player>()
                .HasMany(p => p.halls)
                .WithMany(h => h.players);

            modelBuilder.Entity<Hall>()
                .HasOne(h => h.gameMaster)
                .WithMany()
                .HasForeignKey(h => h.id);
        }
    }
}
