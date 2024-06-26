using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Services
{
    public interface IRestaurantService
    {
        public void DeleteRestaurant(int id);
        public void UpdateRestaurant(int id, RestaurantDto restaurant);
        int CreateRestaurant(CreateRestaurantDto restaurantDto);
        IEnumerable<RestaurantDto> GetAll();
        Task<RestaurantDto> GetRestaurant(int id);
    }
}