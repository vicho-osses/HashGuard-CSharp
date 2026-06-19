using Microsoft.EntityFrameworkCore;
using SecureUserAPI.data;
using SecureUserAPI.services;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor
builder.Services.AddControllers();

// Configuración de la Base de Datos (SQL Server)
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar tus servicios personalizados
builder.Services.AddScoped<AuthService>();

// ACTIVAR SWAGGER (De forma simple, sin pedir librerías extra)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
var app = builder.Build();

// Forzamos a que Swagger responda SIEMPRE
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecureUserAPI v1");
    c.RoutePrefix = string.Empty; // Swagger será la página de inicio
});

app.UseHttpsRedirection();

app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();