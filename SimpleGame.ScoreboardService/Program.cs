using SimpleGame.ScoreboardService.Core.Application.Services;
using SimpleGame.ScoreboardService.Core.Domain.Interfaces;
using SimpleGame.ScoreboardService.Core.Infrastructure.Middleware;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers()
    .AddXmlSerializerFormatters();

// Register the ScoreboardService
builder.Services.AddSingleton<IScoreboardService, ScoreboardService>();

// Register MediatR and the assemblies containing commands/queries and their handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

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