using EstacionaService.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionaService.Service
{
    public class ClientesService
    {
        private readonly Ef.EstacionaServiceContext _sql;

        public ClientesService()
        {
            _sql = new Ef.EstacionaServiceContext();
        }

        public void InserirCliente(Models.ClienteModel cliente)
        {
            cliente.Entrada = DateTime.Now;
            _sql.ClientesAtivosPatios.Add(new Ef.ClientesAtivosPatio()
            {
                Placa = cliente.Placa,
                TipoVeiculo = cliente.TipoVeiculo,
                Descricao = cliente.Descricao,
                Entrada = DateTime.Now,
                Situacao = true
            });
            _sql.SaveChanges();


        }

        public List<Models.ClienteModel> ListarClientes()
        {

            var retorno = from cliente in _sql.ClientesAtivosPatios
                          where cliente.Situacao == true
                          orderby cliente.Id descending
                          select new Models.ClienteModel()
                          {
                              ID = cliente.Id.ToString(),
                              Placa = cliente.Placa,
                              TipoVeiculo = cliente.TipoVeiculo,
                              Descricao = cliente.Descricao,
                              Entrada = cliente.Entrada
                          };

            return retorno.ToList();


        }

        public Models.ClienteModel FechamentoCliente(string id)
        {
           var clienteFechamento = (from cliente in _sql.ClientesAtivosPatios
                                                                          where Convert.ToInt32(id) == cliente.Id
                                                                          select new Models.ClienteModel()
                                                                          {
                                                                              ID = cliente.Id.ToString(),
                                                                              Descricao = cliente.Descricao,
                                                                              Placa = cliente.Placa,
                                                                              TipoVeiculo = cliente.TipoVeiculo,
                                                                              Entrada = cliente.Entrada,
                                                                              Saida = DateTime.Now,
                                                                              Situacao = cliente.Situacao,
                                                                              ValorAPagar = cliente.ValorApagar
                                                                          }).ToList().First();


            if (clienteFechamento.Situacao == false)
                throw new Exception("Cliente não disponivel para esta operação");

            TimeSpan tempoGasto = RegrasDeNegocio.Calculos.TempoGasto(Convert.ToDateTime(clienteFechamento.Entrada), Convert.ToDateTime(clienteFechamento.Saida));
            clienteFechamento.TempoGasto = tempoGasto.TotalMinutes.ToString("N2") + " Minutos";
            clienteFechamento.ValorAPagar = Math.Round(RegrasDeNegocio.Calculos.CalcularFechamento(tempoGasto, clienteFechamento.TipoVeiculo), 2);

            _sql.Pagamentos.Add(new Ef.Pagamento
            {
                Id = Convert.ToInt32(clienteFechamento.ID),
                Placa = clienteFechamento.Placa,
                TipoVeiculo = clienteFechamento.TipoVeiculo,
                TempoGasto = clienteFechamento.TempoGasto,
                Valor = clienteFechamento.ValorAPagar.ToString(),
                DataPag = DateTime.Now.ToString("d")

            });


            _sql.ClientesAtivosPatios.Update(new Ef.ClientesAtivosPatio()
            {
                Id = int.Parse(clienteFechamento.ID),
                Descricao = clienteFechamento.Descricao,
                Entrada = clienteFechamento.Entrada,
                Placa = clienteFechamento.Placa,
                TipoVeiculo = clienteFechamento.TipoVeiculo,
                Saida = clienteFechamento.Saida,
                ValorApagar = clienteFechamento.ValorAPagar,
                TempoGasto = clienteFechamento.TempoGasto,
                Situacao = false
            });


            _sql.SaveChanges();

            return clienteFechamento;

        }

        public string Pagamento(decimal valorPago, decimal valorAReceber, string id)
        {
            var troco = RegrasDeNegocio.Calculos.Troco(valorAReceber, valorPago).ToString("N2");

            return troco;
        }



    }
}
