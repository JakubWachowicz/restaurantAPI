using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI2._0.Models
{
    public class UpdateRestaurantDto
    {

        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }

        [MaxLength(50)]
        public string City { get; set; }


    }
}
