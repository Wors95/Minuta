using System.ComponentModel.DataAnnotations;

namespace SiteRelogios.Models;

public class Relogio
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Marca { get; set; } = string.Empty;

    public string Modelo { get; set; } = string.Empty;

    public decimal Preco { get; set; }
}
