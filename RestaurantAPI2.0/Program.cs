using Microsoft.EntityFrameworkCore;
using NLog;
using RestaurantAPI2._0.Entities;
using RestaurantAPI2._0.Services;
using NLog.Extensions.Logging;
using RestaurantAPI2._0.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Configure NLog
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
});

// Add NLog as the logger provider
builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<IWeatherForecastService,WeatherForecastService>();

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<ExceptionLoggerMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleWare>();
builder.Services.AddDbContext<RestaurantDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionLoggerMiddleware>();
app.UseMiddleware<RequestTimeMiddleWare>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<RestaurantDbContext>();
    await context.Database.MigrateAsync();
    DbSeederService seederService = new DbSeederService(context);
    await seederService.Seed();
}
catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during Migration");
    throw;
}

app.Run();
