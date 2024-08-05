using AutoMapper;
using RestaurantAPI2._0.Models;

namespace RestaurantAPI2._0.Entities
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street));
            CreateMap<Dish, DishDto>();
            CreateMap<Restaurant,CreateRestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street));
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address() { City = dto.City, Street = dto.Street }));
            CreateMap<CreateDishDto, Dish>();
        }
    }
}
