using Book_Keep.Models;
using Book_Keep.Models.Library;
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
        public DbSet<Room> Room { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Shelf> Shelf { get; set; }
        public DbSet<ShelfSlot> ShelfSlot { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(d =>
            {
                d.HasOne(u => u.Department)
                .WithMany(d => d.User)
                .HasForeignKey(u => u.Departmentid);
            });

            modelBuilder.Entity<Section>(d =>
            {
                d.HasOne(s => s.Room)
                .WithMany(s => s.Section)
                .HasForeignKey(s => s.Roomid);
            });

            modelBuilder.Entity<Shelf>(d =>
            {
                d.HasOne(s => s.Section)
                .WithMany(s => s.Shelf)
                .HasForeignKey(s => s.Sectionid);
            });

            modelBuilder.Entity<ShelfSlot>(d =>
            {
                d.HasOne(s => s.Shelf)
                .WithMany(s => s.Shelfslot)
                .HasForeignKey(s => s.Shelfid);
            });

            modelBuilder.Entity<Book>(d =>
            {
                d.HasOne(b => b.Shelfslot)
                .WithOne(b => b.Book)
                .HasForeignKey<Book>(b => b.Shelfslotid);
            });
        }
    }
}
