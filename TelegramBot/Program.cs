using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TelegramBot.DataLayer;
using TelegramBot.DataLayer.Interfaces;
using TelegramBot.DataLayer.Repositories;
using TelegramBot.DataLayer.Models;
using TelegramBot.BusinessLayer.Services;
using TelegramBot.BusinessLayer.Interfaces;
using TelegramBot.BusinessLayer.DTOs;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IUserDBRepository, UserDBRepository>();
builder.Services.AddTransient<IRepository<ChatDB>, ChatDBRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IService<ChatDTO>, ChatService>();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();