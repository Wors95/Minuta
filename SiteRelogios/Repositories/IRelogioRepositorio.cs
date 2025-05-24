using SiteRelogios.Models;

namespace SiteRelogios.Repositories;

public interface IRelogioRepositorio
{
    Task AdicionarAsync(Relogio relogio);
    Task<IEnumerable<Relogio>> ListarTodosAsync();
    Task<Relogio?> BuscarPorIdAsync(Guid id);
    Task AtualizarAsync(Relogio relogio);
    Task RemoverAsync(Guid id);
}
