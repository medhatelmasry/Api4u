using Api4u.Models.Students;
using Api4u.Models.Toons;
using Microsoft.EntityFrameworkCore;

namespace Api4u.Data
{
    public partial class ToonsContext : DbContext
    {
        public ToonsContext()
        {
        }

        public ToonsContext(DbContextOptions<ToonsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}