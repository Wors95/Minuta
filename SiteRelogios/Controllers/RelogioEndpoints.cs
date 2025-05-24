using Microsoft.AspNetCore.Mvc;
using SiteRelogios.Models;
using SiteRelogios.Services;

namespace SiteRelogios.Controllers;

public static class RelogioEndpoints
{
    public static void MapearRelogioEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/relogios", async ([FromBody] Relogio relogio, RelogioServico servico) =>
        {
            await servico.CadastrarRelogioAsync(relogio);
            return Results.Created($"/relogios/{relogio.Id}", relogio);
        });

        app.MapGet("/relogios", async (RelogioServico servico) =>
        {
            var relogios = await servico.ListarRelogiosAsync();
            return Results.Ok(relogios);
        });

        app.MapGet("/relogios/{id:guid}", async (Guid id, RelogioServico servico) =>
        {
            var relogio = await servico.ObterRelogioPorIdAsync(id);
            return relogio is not null ? Results.Ok(relogio) : Results.NotFound();
        });

        app.MapPut("/relogios/{id:guid}", async (Guid id, [FromBody] Relogio relogioAtualizado, RelogioServico servico) =>
        {
            var existente = await servico.ObterRelogioPorIdAsync(id);
            if (existente is null) return Results.NotFound();

            existente.Marca = relogioAtualizado.Marca;
            existente.Modelo = relogioAtualizado.Modelo;
            existente.Preco = relogioAtualizado.Preco;

            await servico.AtualizarRelogioAsync(existente);
            return Results.Ok(existente);
        });

        app.MapDelete("/relogios/{id:guid}", async (Guid id, RelogioServico servico) =>
        {
            var existente = await servico.ObterRelogioPorIdAsync(id);
            if (existente is null) return Results.NotFound();

            await servico.RemoverRelogioAsync(id);
            return Results.NoContent();
        });
    }
}
