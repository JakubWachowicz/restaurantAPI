using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDto dto);
        public DishDto GetDish(int id, int restaurantId);
        public IEnumerable<DishDto> GetAll(int restaurantId);
    }
}