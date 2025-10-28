using DigiShopping.Data;
using DigiShopping.Services;
using DigiShopping.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add memory cache services
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<DigiShoppingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DigiShoppingDb")));
builder.Services.AddScoped<IShoppingCartBAL, ShoppingCartBAL>();
builder.Services.AddTransient<IShoppingCartDAL, ShoppingCartDAL>();
builder.Services.AddTransient<IPointsPromotionDAL, PointsPromotionDAL>();

var app = builder.Build();

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
