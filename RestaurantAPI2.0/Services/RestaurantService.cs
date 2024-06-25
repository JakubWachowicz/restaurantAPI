using AutoMapper;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace RestaurantAPI2._0.Services
{
    public class RestaurantService(RestaurantDbContext context, IMapper mapper) : IRestaurantService
    {
        private readonly IMapper _mapper = mapper;
        private readonly RestaurantDbContext _context = context;

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _context.Restaurants.Include(r => r.Address).Include(r => r.Dishes).ToList();
            if (restaurants is null) return null;
            var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDto;
        }
        public async Task<RestaurantDto> GetRestaurant(int id)
        {
            var restaurant = _context.Restaurants.Include(r => r.Address).Include(r => r.Dishes).FirstOrDefault(r => r.Id == id);
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }


        public bool DeleteRestaurant(int id)
        {
            var restaurant =  _context.Restaurants.FirstOrDefault(r=>r.Id ==id);
            if (restaurant != null)
            {
                context.Remove(restaurant);
                context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public bool UpdateRestaurant(int id,RestaurantDto restaurant)
        {
            var foundRestaurant =  _context.Restaurants.FindAsync(id);
            if (foundRestaurant != null)
            {
                mapper.Map(restaurant, foundRestaurant);
                context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public int CreateRestaurant(CreateRestaurantDto restaurantDto)
        {
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);

            context.Restaurants.AddAsync(restaurant);
            _context.SaveChanges();
            return restaurant.Id;
        }
    }
}
