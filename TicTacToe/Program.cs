
using Microsoft.EntityFrameworkCore;
using TicTacToe;
using TicTacToe.DAL;
using TicTacToe.DAL.Interfaces;
using TicTacToe.DAL.Repos;
using TicTacToe.Service.Interfaces;
using TicTacToe.Service.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connections = builder.Configuration.GetConnectionString("Pg");
builder.Services.AddDbContext<ApplicationContext>(o => o.UseNpgsql(connections));

builder.Services.AddScoped<IGameRepo, GameRepo>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IResultService, ResultService>();


builder.Services.AddControllers();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
