using AutoMapper;

namespace RestaurantAPI2._0.Models
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() {
            CreateMap<Restaurant, Restaurant>();
        }
    }
}
