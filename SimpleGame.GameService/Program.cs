using SimpleGame.GameService.Core.Application.Services;
using SimpleGame.GameService.Core.Domain.Interfaces;
using SimpleGame.GameService.Core.Infrastructure.Extensions;
using SimpleGame.GameService.Core.Infrastructure.Middleware;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger to include the XML comments for documentation
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Register MediatR for CQRS
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Register Application Services
builder.Services.AddScoped<IGameSetupService, GameSetupService>();
builder.Services.AddScoped<IGameLogicService, GameLogicService>();

// Configure HttpClients using extension method (Polly and retries added)
builder.Services.ConfigureHttpClientsWithPolly(builder.Configuration);

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS (Optional) 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
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

// Add custom exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();