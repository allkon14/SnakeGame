using Microsoft.EntityFrameworkCore;
using System;

namespace ORMDal
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DefaultDbContext()
        {
        }

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Database.EnsureCreated();
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated security=True;Initial Catalog=SnakeGame");


                //optionsBuilder.UseSqlServer("Data Source=Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SnakeGame;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Integrated security=True;Initial Catalog=SnakeGame");
                //    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SnakeGame;Trusted_Connection=True;"

            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Games_UserId");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

           // OnModelCreatingPartial(modelBuilder);
        }

       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
