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
        private readonly SqlConnection _sql;
        public ClientesService()
        {
            string stringConexao = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionString")["StringConexao"];
            var conexao = File.ReadAllText(stringConexao);
            _sql = new SqlConnection(conexao);
        }

        public void InserirCliente(Models.ClienteModel cliente)
        {
            var sql = new ClientesData();
            sql.InserirCliente(cliente, _sql);
        }

        public List<Models.ClienteModel> ListarClientes()
        {
            var sql = new ClientesData();
            return sql.ListarClientes(_sql);
        }

        public Models.ClienteModel FechamentoCliente(string placa)
        {
            var sql = new ClientesData();
            var clienteFechamento = sql.SelecionarCliente(placa, _sql);
            clienteFechamento.Saida = DateTime.Now;

            if (clienteFechamento.Situacao == false)
                throw new Exception("Cliente não disponivel para esta operação");


            TimeSpan tempoGasto = RegrasDeNegocio.Calculos.TempoGasto(clienteFechamento.Entrada, clienteFechamento.Saida);
            clienteFechamento.TempoGasto = tempoGasto.TotalMinutes.ToString("N2") + " Minutos";
            clienteFechamento.ValorAPagar = Math.Round(RegrasDeNegocio.Calculos.CalcularFechamento(tempoGasto, clienteFechamento.TipoVeiculo), 2);

            sql.InserirPagamento(clienteFechamento, _sql);

            return clienteFechamento;

        }

        public string Pagamento(decimal valorPago, decimal valorAReceber, string placa)
        {
            var sql = new ClientesData();
            var troco = RegrasDeNegocio.Calculos.Troco(valorAReceber, valorPago).ToString("N2");
            sql.AlterarSituacaoPagamento(true, placa, _sql);

            return troco;
        }

    }
}
