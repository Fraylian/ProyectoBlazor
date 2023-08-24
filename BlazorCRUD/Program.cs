using BlazorCRUD.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CompanyContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("conexion"));
});


builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("NuevasPoliticas", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        ;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("NuevasPoliticas");

app.UseAuthorization();

app.MapControllers();

app.Run();
