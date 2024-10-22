using Microsoft.AspNetCore.Mvc;
using RestaurantAPI2._0.Models;
using RestaurantAPI2._0.Services;

namespace RestaurantAPI2._0.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto registerUserDto)
        {
            accountService.RegisterUser(registerUserDto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult LoginUser([FromBody] LoginUserDto loginUserDto) {

            string token = accountService.GenerateJwt(loginUserDto);
            return Ok(token);
        }
    }
}
