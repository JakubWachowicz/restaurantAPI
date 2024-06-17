namespace RestaurantAPI2._0.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public virtual int RestaurantId { get; set; }
    }
}