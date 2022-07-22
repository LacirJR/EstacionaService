using System;
using System.Collections.Generic;

namespace EstacionaService.Ef
{
    public partial class MensalistaPf
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Placa { get; set; }
        public string TipoVeiculo { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataEntrada { get; set; }
        public decimal? Valor { get; set; }
        public int? Plano { get; set; }
        public bool? Situacao { get; set; }
    }
}
