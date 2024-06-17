using Microsoft.AspNetCore.Mvc;
using RestaurantAPI2._0.Services;

namespace RestaurantAPI2._0.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private IWeatherForecastService _service;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Get([FromQuery] int n, [FromQuery] int min, [FromQuery] int max)
        {
            return n > 0 && max > min?  StatusCode(200,_service.GetForecasts(n, min, max)): StatusCode(400);
        }

        [HttpGet("{id}")]
        public WeatherForecast GetWeatherFromId([FromQuery]int max,[FromRoute]int id)
        {
            return _service.GetForecastInCountry();
        }
        [HttpPost]
        public ActionResult<string> Hello([FromBody]string name)
        {
            return StatusCode(200,$"Hello {name}");
        }
    }
}
