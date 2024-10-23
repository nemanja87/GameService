using SimpleGame.GameServiceApp.Core.Application.Clients.Services;
using SimpleGame.GameServiceApp.Core.Application.Services;
using SimpleGame.GameServiceApp.Core.Domain.Interfaces;
using SimpleGame.GameServiceApp.Core.Infrastructure.Services.ComputerServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register MediatR for CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Register GameSetupService
builder.Services.AddScoped<IGameSetupService, GameSetupService>();

// Register GameLogicService
builder.Services.AddScoped<IGameLogicService, GameLogicService>();

// Configure HttpClient with the Random Number API base URL
builder.Services.AddHttpClient<IRandomNumberService, RandomNumberService>(client =>
{
    var randomNumberApiUrl = builder.Configuration["RandomNumberApiUrl"];
    client.BaseAddress = new Uri(randomNumberApiUrl);
});

builder.Services.AddHttpClient<IComputerServiceClient, ComputerServiceClient>(client =>
{
    var baseUrl = builder.Configuration["ComputerService:BaseUrl"];
    if (string.IsNullOrEmpty(baseUrl))
    {
        throw new InvalidOperationException("ComputerService BaseUrl is not configured");
    }
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// (Optional) Add CORS if needed
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();