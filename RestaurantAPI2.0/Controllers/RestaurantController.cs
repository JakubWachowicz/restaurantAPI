using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : Controller
    {
        private readonly RestaurantDbContext context;
        private readonly IMapper mapper;

        public RestaurantController(RestaurantDbContext context,IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll() 
        {
            var restaurants = context.Restaurants.ToList();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> Get(int id)
        {
            var restaurant = await context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var restaurant = await context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                context.Remove(restaurant);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id,Restaurant restaurant) {
            var foundRestaurant = await context.Restaurants.FindAsync(id);
            if(foundRestaurant!= null)
            {
                mapper.Map(restaurant,foundRestaurant);
                await context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Restaurant restaurant)
        {
            await context.Restaurants.AddAsync(restaurant);
            return Ok();
        }
    }
}
