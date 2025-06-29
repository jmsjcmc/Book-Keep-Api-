using Book_Keep.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Keep
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(d =>
            {
                d.HasOne(u => u.Department)
                .WithMany(d => d.User)
                .HasForeignKey(u => u.Departmentid);
            });
        }
    }
}
