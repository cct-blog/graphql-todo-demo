using DemoService.Entities;
using DemoService.Resolvers;

using Microsoft.EntityFrameworkCore;
using DemoService.Interfaces;
using DemoService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 環境変数を整える
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContextを追加する
var connectionString = builder.Configuration["ConnectionStrings:MySQL"]
    ?? throw new InvalidOperationException("MySQLが空です");
builder.Services.AddDbContext<ToDoDbContext>(options => options.UseMySQL(connectionString));
// DIで使うクラス群
builder.Services
    .AddScoped<ToDoDbContext>()
    .AddScoped<IToDoService, ToDoService>()
    .AddScoped<IToDoRelationService, ToDoRelationService>();
// GraaphQLで使うクラス群
builder.Services.AddGraphQLServer()
    .RegisterService<IToDoService>()
    .RegisterService<IToDoRelationService>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<ToDoPayloadExtend>();

var app = builder.Build();

app.MapGraphQL();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
