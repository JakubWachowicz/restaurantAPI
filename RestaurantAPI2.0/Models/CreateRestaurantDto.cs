using RestaurantAPI2._0.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI2._0.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
    }
}
