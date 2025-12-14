using DealershipAPI.Repositories;
using DealershipAPI.Services;
using DealershipAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Dependency Injection
builder.Services.AddSingleton<ICarRepository, CarRepository>();
builder.Services.AddHostedService<RegistrationCheckerService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<RegistrationHub>("/registrationHub");

app.Run();
