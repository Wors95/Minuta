using Microsoft.EntityFrameworkCore;
using SiteRelogios.Data;
using SiteRelogios.Models;

namespace SiteRelogios.Repositories;

public class RelogioRepositorio : IRelogioRepositorio
{
    private readonly AppDbContext _contexto;

    public RelogioRepositorio(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AdicionarAsync(Relogio relogio)
    {
        _contexto.Relogios.Add(relogio);
        await _contexto.SaveChangesAsync();
    }

    public async Task<IEnumerable<Relogio>> ListarTodosAsync()
    {
        return await _contexto.Relogios.ToListAsync();
    }

    public async Task<Relogio?> BuscarPorIdAsync(Guid id)
    {
        return await _contexto.Relogios.FindAsync(id);
    }

    public async Task AtualizarAsync(Relogio relogio)
    {
        _contexto.Relogios.Update(relogio);
        await _contexto.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid id)
    {
        var relogio = await BuscarPorIdAsync(id);
        if (relogio is not null)
        {
            _contexto.Relogios.Remove(relogio);
            await _contexto.SaveChangesAsync();
        }
    }
}
