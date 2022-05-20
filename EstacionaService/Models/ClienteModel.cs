using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionaService.Models
{
  public class ClienteModel
  {
    public string Placa { get; set; }
    public string ID { get; set; }
    public string Descricao { get; set; }
    public string TipoVeiculo { get; set; }
    public string TempoGasto { get; set; }
    public DateTime Entrada { get; set; }
    public DateTime Saida { get; set; }
    public bool Situacao { get; set; }
    public decimal ValorAPagar { get; set; }

  }



}
