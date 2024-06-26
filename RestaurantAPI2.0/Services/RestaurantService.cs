using AutoMapper;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestaurantAPI2._0.Exceptions;
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


        public void DeleteRestaurant(int id)
        {
            var restaurant =  _context.Restaurants.FirstOrDefault(r=>r.Id ==id);
            if (restaurant == null) throw new NotFountException("Restaurant not found");
            
            context.Remove(restaurant);
            context.SaveChangesAsync();
        }
        public void UpdateRestaurant(int id,RestaurantDto restaurant)
        {
            var foundRestaurant =  _context.Restaurants.FindAsync(id);

            if (foundRestaurant == null) throw new NotFountException("Restaurant not found");
            
           
            mapper.Map(restaurant, foundRestaurant);
            context.SaveChangesAsync();
         
        
    
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
