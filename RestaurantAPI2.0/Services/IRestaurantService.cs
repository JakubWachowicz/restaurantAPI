using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Services
{
    public interface IRestaurantService
    {
        public bool DeleteRestaurant(int id);
        public bool UpdateRestaurant(int id, RestaurantDto restaurant);
        int CreateRestaurant(CreateRestaurantDto restaurantDto);
        IEnumerable<RestaurantDto> GetAll();
        Task<RestaurantDto> GetRestaurant(int id);
    }
}