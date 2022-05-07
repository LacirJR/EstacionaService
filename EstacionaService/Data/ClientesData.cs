using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionaService.Data
{
    public class ClientesData
    {


        public void InserirCliente(Models.ClienteModel cliente, SqlConnection _sql)
        {

            _sql.Open();

            string query = "INSERT INTO Clientes (Cpf," +
                                                " Placa," +
                                                " TipoVeiculo," +
                                                " Nome," +
                                                " Entrada," +
                                                " Situacao)" +

                                   "VALUES      (@Cpf," +
                                               "  @Placa," +
                                                " @TipoVeiculo," +
                                                " @Nome," +
                                                " @Entrada," +
                                                " @Situacao)";

            using (SqlCommand cmd = new SqlCommand(query, _sql))
            {
                cmd.Parameters.AddWithValue("Cpf", cliente.CPF);
                cmd.Parameters.AddWithValue("Placa", cliente.Placa.ToUpper());
                cmd.Parameters.AddWithValue("TipoVeiculo", cliente.TipoVeiculo.ToUpper());
                cmd.Parameters.AddWithValue("Nome", cliente.Nome.ToUpper());
                cmd.Parameters.AddWithValue("Entrada", DateTime.Now);
                cmd.Parameters.AddWithValue("Situacao", true);

                cmd.ExecuteNonQuery();
            }

            _sql.Close();

        }

        public List<Models.ClienteModel> ListarClientes(SqlConnection _sql)
        {
            var listaClientes = new List<Models.ClienteModel>();
            _sql.Open();

            string query = "Select Nome, Placa, TipoVeiculo, Entrada  From Clientes Where Situacao = 1";

            using (var cmd = new SqlCommand(query, _sql))
            {
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var cliente = new Models.ClienteModel();
                    cliente.Nome = Convert.ToString(rdr["Nome"]);
                    cliente.Placa = Convert.ToString(rdr["Placa"]);
                    cliente.TipoVeiculo = Convert.ToString(rdr["TipoVeiculo"]);
                    cliente.Entrada = Convert.ToDateTime(rdr["Entrada"]);

                    listaClientes.Add(cliente);

                }
            }
            return listaClientes;
        }

        public Models.ClienteModel SelecionarCliente(string placa, SqlConnection _sql)
        {
            var cliente = new Models.ClienteModel();
            _sql.Open();
            string query = "SELECT Nome, CPF, Placa, TipoVeiculo, Entrada, Saida, ValorAPagar, Situacao FROM Clientes WHERE Placa = @Placa " +
                "Update Clientes SET Situacao = 0 Where Placa = @Placa";

            using (var cmd = new SqlCommand(query, _sql))
            {
                cmd.Parameters.AddWithValue("@Placa", placa);
                var rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cliente.Nome = Convert.ToString(rdr["Nome"]);
                    cliente.CPF = Convert.ToString(rdr["CPF"]);
                    cliente.Placa = Convert.ToString(rdr["Placa"]);
                    cliente.TipoVeiculo = Convert.ToString(rdr["TipoVeiculo"]);
                    cliente.Entrada = Convert.ToDateTime(rdr["Entrada"]);
                    cliente.Situacao = Convert.ToBoolean(rdr["Situacao"]);
                    cliente.ValorAPagar = (rdr["ValorAPagar"]) == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ValorAPagar"]);
                    cliente.Saida = (rdr["Saida"]) == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rdr["Saida"]);
                }
            }

            _sql.Close();

            return cliente;
        }
        public void InserirPagamento(Models.ClienteModel cliente, SqlConnection _sql)
        {

            _sql.Open();

            string query = "INSERT INTO Pagamento (Cpf," +
                                                " Placa," +
                                                " TipoVeiculo," +
                                                " TempoGasto," +
                                                " ValorPago," +
                                                " DataPagamento," +
                                                " Situacao)" +

                                   "VALUES      (@Cpf," +
                                               "  @Placa," +
                                                " @TipoVeiculo," +
                                                " @TempoGasto," +
                                                " @Valor," +
                                                " @DataPagamento," +
                                                " @Situacao) ";

            query += " UPDATE Cliente SET Saida = @Saida, ValorAPagar = @ValorAPagar, TempoGasto = @TempoGasto Where Placa = @Placa";

            using (SqlCommand cmd = new SqlCommand(query, _sql))
            {
                cmd.Parameters.AddWithValue("Cpf", cliente.CPF);
                cmd.Parameters.AddWithValue("Placa", cliente.Placa.ToUpper());
                cmd.Parameters.AddWithValue("TipoVeiculo", cliente.TipoVeiculo.ToUpper());
                cmd.Parameters.AddWithValue("TempoGasto", cliente.TempoGasto.ToUpper());
                cmd.Parameters.AddWithValue("ValorPago", cliente.ValorAPagar);
                cmd.Parameters.AddWithValue("Saida", cliente.Saida);
                cmd.Parameters.AddWithValue("ValorAPagar", cliente.ValorAPagar);
                cmd.Parameters.AddWithValue("DataPagamento", DateTime.Now.ToString("d"));
                cmd.Parameters.AddWithValue("Situacao", false);

                cmd.ExecuteNonQuery();
            }

            _sql.Close();

        }

        public void AlterarSituacaoPagamento(bool situacao, string placa, SqlConnection _sql)
        {
            _sql.Open();

            string query = "UPDATE Pagamento SET Situacao = @Situacao WHERE Placa = @Placa";
            using (SqlCommand cmd = new SqlCommand(query, _sql))
            {
                cmd.Parameters.AddWithValue("Placa", placa.ToUpper());
                cmd.Parameters.AddWithValue("Situacao", situacao);

                cmd.ExecuteNonQuery();
            }

            _sql.Close();


        }
    }
}
