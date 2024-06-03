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
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Roll> Rolls { get; set; }
        public DbSet<Dice> Dices { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Contacts)
                .WithMany();

            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Player>().Property<string>("UserId");

            modelBuilder.Entity<Player>()
                .HasOne(p => p.User)
                .WithOne(u => u.Player)
                .HasForeignKey<Player>("UserId") //Type is necessary because it is a one to one relationship
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

            modelBuilder.Entity<Contact>().Property<string>("RequestorId");
            modelBuilder.Entity<Contact>().Property<string>("RecipientId");

            modelBuilder.Entity<Contact>()
                .HasKey("RequestorId", "RecipientId");
            
            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Requestor)
                .WithMany()
                .HasForeignKey("RequestorId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.Recipient)
                .WithMany()
                .HasForeignKey("RecipientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contact>()
                .Property(f => f.Status)
                .HasConversion<int>();
        }
    }
}
