using System.Text;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
builder.Services.AddScoped<ICompradorRepository, CompradorRepository>();
builder.Services.AddScoped<IProductoRepository,  ProductoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
//BaseRepositories
builder.Services.AddScoped<IBaseRepository<Producto>, BaseRepository<Producto>>();
builder.Services.AddScoped<IBaseRepository<Vendedor>, BaseRepository<Vendedor>>();
builder.Services.AddScoped<IBaseRepository<Comprador>, BaseRepository<Comprador>>();
builder.Services.AddScoped<IBaseRepository<Usuario>, BaseRepository<Usuario>>();
//Services
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IVendedorService, VendedorService>();
builder.Services.AddScoped<ICompradorService, CompradorService>();
builder.Services.AddScoped<IAutenticacionService, AutenticacionService>();

#region Autenticacion
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Introduzca el token JWT como: Bearer {token}"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});
//configurar las opciones para la clase AutenticacionServiceOptions
builder.Services.Configure<AutenticacionService.AutenticacionServiceOptions>(
    builder.Configuration.GetSection("Authentication"));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });



// configuración de autorización basada en roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Comprador", policy => policy.RequireRole(Userrole.Comprador.ToString(), Userrole.Sysadmin.ToString()));
    options.AddPolicy("Vendedor", policy => policy.RequireRole(Userrole.Vendedor.ToString(), Userrole.Sysadmin.ToString()));
    options.AddPolicy("Sysadmin", policy => policy.RequireRole(Userrole.Sysadmin.ToString()));


});
#endregion

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
