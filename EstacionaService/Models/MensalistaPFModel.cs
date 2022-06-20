using System;
namespace EstacionaService.Models
{
  public class MensalistaPFModel
  {

    public string CPF { get; set; }
    public string Nome { get; set; }
    public string Placa { get; set; }
    public string TipoVeiculo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataEntrada { get; set; }
    public int Plano { get; set; }
    public decimal Valor { get; set; }
    public bool Situacao { get; set; }
  }
}
