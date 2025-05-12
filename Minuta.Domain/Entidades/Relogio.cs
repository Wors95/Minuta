using Minuta.Domain.Enums;

namespace Minuta.Domain.Entidades
{
    public class Relogio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public TipoRelogio Tipo { get; set; }
    }
}
