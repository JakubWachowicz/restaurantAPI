using RestaurantAPI2._0.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI2._0.Models
{
    public class RegisterUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public int RoleId { get; set; } = 1;
    } 
}
