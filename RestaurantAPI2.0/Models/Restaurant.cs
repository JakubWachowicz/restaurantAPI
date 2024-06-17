namespace RestaurantAPI2._0.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AdresId { get; set; }
        public virtual Address Address {get;set; }
        public virtual List<Dish> Dishes { get; set; }
    }
}
