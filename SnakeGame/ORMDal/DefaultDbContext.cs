using Microsoft.EntityFrameworkCore;
using System;

namespace ORMDal
{
    public partial class DefaultDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public virtual DbSet<Game> Games { get; set; }

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
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Integrated security=True;Initial Catalog=SnakeGame");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.GameDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Games_Name");
            });

           

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Password).IsRequired();



            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
