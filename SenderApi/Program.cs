using SenderApi;
using SenderApi.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add controllers services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(); // Add authorization services

// Add MassTransit services
builder.Services.AddServices(); // Add this line to invoke the AddServices method

// Add Db Context Here
builder.Services.AddDbContext<MessageDbContext>(options =>
    options.UseSqlServer("Server=SherifAbdullah\\MSSQLSERVER01;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); // Map controllers in the application

app.Run();
