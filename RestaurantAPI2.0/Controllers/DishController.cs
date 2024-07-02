using Microsoft.AspNetCore.Mvc;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Models;
using RestaurantAPI2._0.Services;

namespace RestaurantAPI2._0.Controllers 
{

    [ApiController]
    [Route("api/restaurant/{restaurantId}/dish")]
    public class DishController : ControllerBase
    {
        private readonly IDishService dishService;

        public DishController(IDishService dishService)
        {
            this.dishService = dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, CreateDishDto dto) {
            var id = dishService.Create(restaurantId, dto);
            return Created($"api/restaurant/{restaurantId}/dish/{id}", null);
        }
        [HttpGet("{dishId}")]
        public ActionResult GetDish([FromRoute] int dishId, [FromRoute] int restaurantId)
        {
            return Ok(dishService.GetDish(dishId, restaurantId));
        }
        [HttpGet]
        public ActionResult GetAll([FromRoute] int restaurantId)
        {
            return Ok(dishService.GetAll(restaurantId));
        }
    }
} 
