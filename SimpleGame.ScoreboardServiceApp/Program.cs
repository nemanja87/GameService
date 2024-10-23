using SimpleGame.ScoreboardServiceApp.Core.Application.Services;
using SimpleGame.ScoreboardServiceApp.Core.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();

// Register the ScoreboardService
builder.Services.AddSingleton<IScoreboardService, ScoreboardService>();

// Register MediatR and the assemblies containing commands/queries and their handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();