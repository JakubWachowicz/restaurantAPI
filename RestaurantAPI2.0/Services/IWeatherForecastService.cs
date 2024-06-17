namespace RestaurantAPI2._0.Services
{
    public interface IWeatherForecastService
    {
        WeatherForecast GetForecastInCountry();
        IEnumerable<WeatherForecast> GetForecasts(int n,int min,int max);
    }
}