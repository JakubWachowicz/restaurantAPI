using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto registerUserDto);
    }
}