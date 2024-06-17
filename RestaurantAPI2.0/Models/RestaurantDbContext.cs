using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI2._0.Models
{
    public class RestaurantDbContext:DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Dish> Dishes { get; set; } 
         
    }
}
