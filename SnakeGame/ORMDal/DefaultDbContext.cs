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
                optionsBuilder.UseSqlServer("Data Source=localhost;Integrated security=True;Initial Catalog=LessonApplication");
            }
        }
    }
}
