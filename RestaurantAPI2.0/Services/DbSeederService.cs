using Microsoft.EntityFrameworkCore;
using RestaurantAPI2._0.Entities;
using System.Reflection.Metadata.Ecma335;

namespace RestaurantAPI2._0.Services
{
    public class DbSeederService
    {
        private readonly RestaurantDbContext _context;

        public DbSeederService(RestaurantDbContext context)
        {
            this._context = context;
        }

        private IEnumerable<Restaurant> GenerateRestaurants()
        {
            var address1 = new Address { Street = "123 Main St", City = "Townsville",};
            var address2 = new Address { Street = "456 Elm St", City = "Cityplace", };

            // Create sample dishes for restaurant 1
            var dish1 = new Dish { Name = "Spaghetti", Description = "Classic spaghetti with tomato sauce", Price = 12.99m};
            var dish2 = new Dish { Name = "Pizza", Description = "Cheese pizza with a crispy crust", Price = 10.99m };

            // Create sample dishes for restaurant 2
            var dish3 = new Dish {Name = "Burger", Description = "Juicy beef burger with lettuce and tomato", Price = 8.99m };
            var dish4 = new Dish { Name = "Fries", Description = "Crispy french fries", Price = 3.99m };

            // Create sample restaurants
            var restaurant1 = new Restaurant
            {
  
                Name = "Italian Bistro",
                Description = "A cozy place for Italian cuisine",
                AdresId = 1,
                Address = address1,
                Dishes = new List<Dish> { dish1, dish2 }
            };

            var restaurant2 = new Restaurant
            {

                Name = "Burger Joint",
                Description = "Best burgers in town",
                AdresId = 2,
                Address = address2,
                Dishes = new List<Dish> { dish3, dish4 }
            };

            return [restaurant1, restaurant2];
        }

        public Task Seed()
        {
            //If dataBase is empty seed it with mock data
            if (_context.Database.CanConnect())
            {
                if (!_context.Restaurants.Any())
                {
                    var restaurants = GenerateRestaurants();
                    _context.Restaurants.AddRangeAsync(restaurants);
                    _context.SaveChangesAsync();
                }
            } return Task.CompletedTask;
        }
    }
}
