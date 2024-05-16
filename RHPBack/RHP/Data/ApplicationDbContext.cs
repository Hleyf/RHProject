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
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Player>()
                .HasOne(p => p.User)
                .WithOne(u => u.Player)
                .HasForeignKey<Player>(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
             
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Halls)
                .WithMany(h => h.Players);

            modelBuilder.Entity<Hall>()
                .HasOne(h => h.GameMaster)
                .WithMany()
                .HasForeignKey(h => h.Id);
        }
    }
}
