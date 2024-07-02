using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Exceptions;
using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = context.Restaurants.Find(restaurantId);
            if (restaurant is null) throw new NotFountException("restaurant not found");
            var dish = mapper.Map<Dish>(dto);
            context.Dishes.Add(dish);
            context.SaveChanges();
            return dish.Id;
        }
        public DishDto GetDish(int id,int restaurantId)
        {
            var restaurant = context.Restaurants.Find(restaurantId);
            if (restaurant is null) throw new NotFountException("restaurant not found");
            var dish = context.Dishes.Find(id);
            if(dish is null || dish.RestaurantId != restaurantId) throw new NotFountException("dish not found");
            return mapper.Map<DishDto>(dish);
        }
        public IEnumerable<DishDto> GetAll(int restaurantId)
        {
            var restaurant = context.Restaurants.Include(r=>r.Dishes).FirstOrDefault(r=>r.Id == restaurantId);
            if (restaurant is null) throw new NotFountException("restaurant not found");
            return mapper.Map<List<DishDto>>(restaurant.Dishes);
        }

    }
}
