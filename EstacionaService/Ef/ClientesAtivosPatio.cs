using System;
using System.Collections.Generic;

namespace EstacionaService.Ef
{
    public partial class ClientesAtivosPatio
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string TipoVeiculo { get; set; }
        public string Descricao { get; set; }
        public string TempoGasto { get; set; }
        public DateTime? Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public decimal? ValorApagar { get; set; }
        public bool? Situacao { get; set; }

        public virtual Pagamento Pagamento { get; set; }
    }
}
