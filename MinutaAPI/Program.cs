using Minuta.Domain.Entidades;
using Minuta.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var db = new Database();
db.Initialize();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minuta API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint para adicionar um novo relógio (POST)
app.MapPost("/relogio", async ([FromBody] Relogio novoRelogio, Database db) =>
{
    await db.AdicionarRelogio(novoRelogio);
    return Results.Created($"/relogio/{novoRelogio.Id}", novoRelogio);
})
.WithName("AdicionarRelogio")
.WithOpenApi();


// Endpoint para listar todos os relógios (GET)
app.MapGet("/relogios", (Database db) =>
{
    var relogios = db.ListarRelogios();
    return Results.Ok(relogios);
})
.WithName("ListarRelogios")
.WithOpenApi();

app.Run();
