using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ShuttleForecast.Application.Common.Contracts;
using ShuttleForecast.Application.GetForecast;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IForecastUseCase, ForecastUseCase>();
builder.Services.AddScoped<IForecastProviderClient, MeteoForecastProvider>();

builder.Services.AddScoped<IForecastRepository, ForecastRepository>();
builder.Services.AddScoped<ICache, InMemoryCache>();

builder.Services.AddDbContext<ForecastDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<MeteoForecastProvider>(client => { client.Timeout = TimeSpan.FromSeconds(3); });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ForecastDbContext>();

    if (db.Database.EnsureCreated())
        db.Database.Migrate();
}

app.MapControllers();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();