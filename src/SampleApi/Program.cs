using Sample.Api;
using Sample.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddNewtonsoftJson()
    .Services

    .AddAutoMapper(typeof(MapperProfile))
    .AddSingleton<IForecastService, RandomForecastService>()
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Local Dev Environment setup.
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
