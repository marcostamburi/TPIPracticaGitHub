using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Context y Sql
// Configure the SQLite connection
var connection = new SqliteConnection("Data Source=Proyecto-Web-Api.db");
connection.Open();

builder.Services.AddDbContext<ApplicationContext>(dbContextOptions => dbContextOptions.UseSqlite(connection));


//Repositories
builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
//BaseRepositories
builder.Services.AddScoped<IBaseRepository<Vendedor>, BaseRepository<Vendedor>>();
//Services
builder.Services.AddScoped<IVendedorService, VendedorService>();


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
