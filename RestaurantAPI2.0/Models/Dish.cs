﻿using System.Runtime.InteropServices.Marshalling;

namespace RestaurantAPI2._0.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId{get;set;}
        public virtual Restaurant Restaurant { get; set; }  
    }
}