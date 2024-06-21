using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RestaurantAPI2._0.Models
{
    public class RestaurantDbContext:DbContext
    {
        private readonly string _connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public RestaurantDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Dish>()
                .Property(r =>r.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Address>()
                .Property(r=>r.Street).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Address>()
                .Property(r => r.City).IsRequired().HasMaxLength(50);

        }
    }
}
