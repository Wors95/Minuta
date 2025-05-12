using Minuta.Domain.Entidades;
using Minuta.Infrastructure.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Registra a classe Database como serviço singleton
builder.Services.AddSingleton<Database>();

// Adiciona serviços para documentação Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minuta API", Version = "v1" });
});

var app = builder.Build();

// Inicializa o banco de dados (usando injeção de dependência)
var db = app.Services.GetRequiredService<Database>();
db.Initialize();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoint POST: adiciona um novo relógio
app.MapPost("/relogio", async ([FromBody] Relogio novoRelogio, [FromServices] Database db) =>
{
    await db.AdicionarRelogioAsync(novoRelogio);
    return Results.Created($"/relogio/{novoRelogio.Id}", novoRelogio);
})
.WithName("AdicionarRelogio")
.WithOpenApi();

// Endpoint GET: lista todos os relógios
app.MapGet("/relogios", (Database db) =>
{
    var relogios = db.ListarRelogios();
    return Results.Ok(relogios);
})
.WithName("ListarRelogios")
.WithOpenApi();

app.Run();
