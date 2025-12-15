using DealershipAPI.Repositories;
using DealershipAPI.Services;
using DealershipAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSignalR(); 

// Dependency Injection
builder.Services.AddSingleton<ICarRepository, CarRepository>();
builder.Services.AddHostedService<RegistrationCheckerService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:5174")
              ); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();
app.MapHub<RegistrationHub>("/registrationHub");

app.Run();
