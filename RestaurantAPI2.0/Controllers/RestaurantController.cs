using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;
using RestaurantAPI2._0.Services;
using System.Reflection.Metadata.Ecma335;

namespace RestaurantAPI2._0.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;
        private readonly IRestaurantService restaurantService;

        public RestaurantController(RestaurantDbContext context,IMapper mapper,IRestaurantService restaurantService) {
            this.context = context;
            this.mapper = mapper;
            this.restaurantService = restaurantService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll() 
        {
            return Ok(restaurantService.GetAll());
        }

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
            var isDeleted = restaurantService.DeleteRestaurant(id);
            if (isDeleted) return Ok();
            return NotFound();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id,[FromBody]RestaurantDto restaurant) {
            if(!ModelState.IsValid) return BadRequest(ModelState); 
            var isUpdated = restaurantService.UpdateRestaurant(id, restaurant);
            if (isUpdated) { return Ok(); }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRestaurantDto restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = restaurantService.CreateRestaurant(restaurant);
            return Created($"/api/restaurant/{id}",null);
        }
    }
}
