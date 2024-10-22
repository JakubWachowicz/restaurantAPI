using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Exceptions;
using RestaurantAPI2._0.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI2._0.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext context;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(RestaurantDbContext context,IPasswordHasher<User> passwordHasher,AuthenticationSettings authenticationSettings)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }
        public void RegisterUser(RegisterUserDto registerUserDto)
        {
            User user = new User() { Email = registerUserDto.Email, Name = registerUserDto.UserName, RoleId = registerUserDto.RoleId};
            var hashedPassword = passwordHasher.HashPassword(user, registerUserDto.Password);
            user.PasswordHash = hashedPassword;
            context.Users.Add(user);
            context.SaveChanges();
        }
        public string GenerateJwt(LoginUserDto loginUserDto)
        {
            var user = context.Users.Include(u=>u.Role).FirstOrDefault(u => u.Email == loginUserDto.Email);
            if (user == null) 
            {
                throw new BadCredentialsException("Invalid email or password");      
            }
            var hashedPassword = passwordHasher.HashPassword(user, loginUserDto.Password);
            var result = passwordHasher.VerifyHashedPassword(user,user.PasswordHash, hashedPassword);
            if (result == PasswordVerificationResult.Failed) 
            {
                throw new BadCredentialsException("Invalid email or password");
            }
            var claims = new List<Claim>() { 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                new Claim(ClaimTypes.Name, user.Name), 
                new Claim(ClaimTypes.Role, user.Role.Name) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,authenticationSettings.JwtIssuer,claims,expires: expires,signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
