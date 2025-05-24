using SiteRelogios.Models;
using SiteRelogios.Repositories;

namespace SiteRelogios.Services;

public class RelogioServico
{
    private readonly IRelogioRepositorio _repositorio;

    public RelogioServico(IRelogioRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task CadastrarRelogioAsync(Relogio relogio) =>
        await _repositorio.AdicionarAsync(relogio);

    public async Task<IEnumerable<Relogio>> ListarRelogiosAsync() =>
        await _repositorio.ListarTodosAsync();

    public async Task<Relogio?> ObterRelogioPorIdAsync(Guid id) =>
        await _repositorio.BuscarPorIdAsync(id);

    public async Task AtualizarRelogioAsync(Relogio relogio) =>
        await _repositorio.AtualizarAsync(relogio);

    public async Task RemoverRelogioAsync(Guid id) =>
        await _repositorio.RemoverAsync(id);
}
