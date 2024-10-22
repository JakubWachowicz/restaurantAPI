using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;
using RestaurantAPI2._0.Services;

namespace RestaurantAPI2._0.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IRestaurantService restaurantService;
        private readonly ILogger<RestaurantService> logger;

        public RestaurantController(RestaurantDbContext context,IMapper mapper,IRestaurantService restaurantService,ILogger<RestaurantService> logger) {
            this.context = context;
            this.mapper = mapper;
            this.restaurantService = restaurantService;
            this.logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll() 
        {
            return Ok(restaurantService.GetAll());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> Get(int id)
        {
            var restaurant = restaurantService.GetRestaurant(id);
            if(restaurant == null) return NotFound();
            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            logger.LogError($"Restaurant with id: {id} DELETE action invoked");
            restaurantService.DeleteRestaurant(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id,[FromBody]RestaurantDto restaurant) {
            if(!ModelState.IsValid) return BadRequest(ModelState); 
            restaurantService.UpdateRestaurant(id, restaurant);
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<ActionResult> Post([FromBody] CreateRestaurantDto restaurant)
        {
            var id = restaurantService.CreateRestaurant(restaurant);
            return Created($"/api/restaurant/{id}",null);
        }
    }
}
