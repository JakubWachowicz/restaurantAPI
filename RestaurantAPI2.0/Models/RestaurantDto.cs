﻿using RestaurantAPI2._0.Entities;

namespace RestaurantAPI2._0.Models
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public List<DishDto> Dishes { get; set; }
    }
}