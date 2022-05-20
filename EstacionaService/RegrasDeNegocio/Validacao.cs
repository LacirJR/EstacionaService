using System;

namespace EstacionaService.RegrasDeNegocio
{
  public class Validacao
  {
    public static void ValidarEntradaVeiculo(Models.ClienteModel cliente)
    {
      if (string.IsNullOrEmpty(cliente.Descricao) || string.IsNullOrEmpty(cliente.Placa) || string.IsNullOrEmpty(cliente.TipoVeiculo))
      {
        throw new InvalidOperationException("O(s) campo(s) não pode(m) ser nulo(s) ou vazio(s).");
      }

      if (cliente.Placa.Length != 7)
        throw new InvalidOperationException("Placa deve conter 7 caracteres.");



    }
  }
}