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
        public DbSet<ContactRequest> ContactRequest { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Contacts)
                .WithMany();

            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Player>().Property<int>("UserId");

            modelBuilder.Entity<Player>()
                .HasOne(p => p.User)
                .WithOne(u => u.Player)
                .HasForeignKey("UserId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
             
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Halls)
                .WithMany(h => h.Players);

            modelBuilder.Entity<Hall>().Property<int>("GMId"); //Shadow property is used to store the foreign key but not exposed in the model

            modelBuilder.Entity<Hall>()
                .HasOne(h => h.GameMaster)
                .WithMany()
                .HasConstraintName("FK_GM")
                .HasForeignKey("GMId")
                .IsRequired();
            

            modelBuilder.Entity<ContactRequest>().Property<int>("FromId");
            modelBuilder.Entity<ContactRequest>().Property<int>("ToId");

            modelBuilder.Entity<ContactRequest>()
                .HasOne(cr => cr.From)
                .WithMany()
                .HasConstraintName("FK_UserFrom")
                .HasForeignKey("FromId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContactRequest>()
                .HasOne(cr => cr.To)
                .WithMany()
                .HasConstraintName("FK_UserTo")
                .HasForeignKey("ToId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
