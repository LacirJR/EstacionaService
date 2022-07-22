using System;
using System.Collections.Generic;

namespace EstacionaService.Ef
{
    public partial class Pagamento
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string TipoVeiculo { get; set; }
        public string TempoGasto { get; set; }
        public string Valor { get; set; }
        public string DataPag { get; set; }

        public virtual ClientesAtivosPatio IdNavigation { get; set; }
    }
}
