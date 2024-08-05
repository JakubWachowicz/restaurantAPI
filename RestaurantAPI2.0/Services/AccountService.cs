using Microsoft.AspNetCore.Identity;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext context;
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountService(RestaurantDbContext context,IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            User user = new User() { Email = registerUserDto.Email, Name = registerUserDto.UserName, RoleId = registerUserDto.RoleId};
            var hashedPassword = passwordHasher.HashPassword(user, registerUserDto.Password);
            user.PasswordHash = hashedPassword;
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
