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

      string query = "INSERT INTO ClientesAtivosPatio (TipoVeiculo," +
                                          " Descricao," +
                                          " Entrada," +
                                          " Placa," +
                                          " Situacao)" +

                             "VALUES     (@TipoVeiculo," +
                                          " @Descricao," +
                                          " @Entrada," +
                                          " @Placa," +
                                          " @Situacao)";

      using (SqlCommand cmd = new SqlCommand(query, _sql))
      {
        cmd.Parameters.AddWithValue("Descricao", cliente.Descricao.ToUpper());
        cmd.Parameters.AddWithValue("Placa", cliente.Placa.ToUpper());
        cmd.Parameters.AddWithValue("TipoVeiculo", cliente.TipoVeiculo.ToUpper());
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

      string query = "Select REPLICATE('0', 9 - LEN(ID)) + CAST(ID AS varchar) AS 'ID', Placa, Descricao, TipoVeiculo, Entrada  From ClientesAtivosPatio Where Situacao = 1  ORDER BY ID DESC;";

      using (var cmd = new SqlCommand(query, _sql))
      {

        var rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
          var cliente = new Models.ClienteModel();
          cliente.ID = Convert.ToString(rdr["ID"]);
          cliente.Placa = Convert.ToString(rdr["Placa"]);
          cliente.TipoVeiculo = Convert.ToString(rdr["TipoVeiculo"]);
          cliente.Descricao = Convert.ToString(rdr["Descricao"]);
          cliente.Entrada = Convert.ToDateTime(rdr["Entrada"]);

          listaClientes.Add(cliente);

        }
      }
      return listaClientes;
    }

    public Models.ClienteModel SelecionarCliente(string id, SqlConnection _sql)
    {
      var cliente = new Models.ClienteModel();
      _sql.Open();
      string query = "SELECT ID, Descricao, Placa, TipoVeiculo, Entrada, Saida, ValorAPagar, Situacao FROM ClientesAtivosPatio WHERE ID = @ID " +
          "Update ClientesAtivosPatio SET Situacao = 0 Where ID = @ID";

      using (var cmd = new SqlCommand(query, _sql))
      {
        cmd.Parameters.AddWithValue("ID", id);
        var rdr = cmd.ExecuteReader();

        if (rdr.Read())
        {
          cliente.ID = Convert.ToString(rdr["ID"]);
          cliente.Descricao = Convert.ToString(rdr["Descricao"]);
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

      string query = "INSERT INTO Pagamento (ID," +
                                          " Placa," +
                                          " TipoVeiculo," +
                                          " TempoGasto," +
                                          " Valor," +
                                          " DataPag)" +

                             "VALUES      (@ID," +
                                         "  @Placa," +
                                          " @TipoVeiculo," +
                                          " @TempoGasto," +
                                          " @Valor," +
                                          " @DataPag)";

      query += " UPDATE ClientesAtivosPatio SET Saida = @Saida, ValorAPagar = @ValorAPagar, TempoGasto = @TempoGasto, Situacao = 0 Where ID = @ID";

      using (SqlCommand cmd = new SqlCommand(query, _sql))
      {
        cmd.Parameters.AddWithValue("ID", cliente.ID);
        cmd.Parameters.AddWithValue("Placa", cliente.Placa.ToUpper());
        cmd.Parameters.AddWithValue("TipoVeiculo", cliente.TipoVeiculo.ToUpper());
        cmd.Parameters.AddWithValue("TempoGasto", cliente.TempoGasto.ToUpper());
        cmd.Parameters.AddWithValue("Valor", cliente.ValorAPagar.ToString());
        cmd.Parameters.AddWithValue("Saida", cliente.Saida);
        cmd.Parameters.AddWithValue("ValorAPagar", cliente.ValorAPagar);
        cmd.Parameters.AddWithValue("DataPag", DateTime.Now.ToString("d"));


        cmd.ExecuteNonQuery();
      }

      _sql.Close();

    }

    public void AlterarSituacaoPagamento(bool situacao, string id, SqlConnection _sql)
    {
      _sql.Open();

      string query = "UPDATE Pagamento SET Situacao = @Situacao WHERE Id = @Id";
      using (SqlCommand cmd = new SqlCommand(query, _sql))
      {
        cmd.Parameters.AddWithValue("Id", int.Parse(id));
        cmd.Parameters.AddWithValue("Situacao", situacao);

        cmd.ExecuteNonQuery();
      }

      _sql.Close();


    }

    public void CadastrarMensalista(Models.MensalistaPFModel mensalista, SqlConnection _sql)
    {
      _sql.Open();

      string query = "INSERT INTO MensalistaPF (CPF," +
                                          " Nome," +
                                          " Placa," +
                                          " TipoVeiculo," +
                                          " Descricao," +
                                          " DataEntrada," +
                                          " Plano," +
                                          " Valor," +
                                          " Situacao)" +

                             "VALUES      (@CPF," +
                                         "  @Nome," +
                                          " @Placa," +
                                          " @TipoVeiculo," +
                                          " @Descricao," +
                                          " @DataEntrada," +
                                          " @Plano," +
                                          " @Valor," +
                                          " @Situacao)";

      using (SqlCommand cmd = new SqlCommand(query, _sql))
      {

        cmd.Parameters.AddWithValue("CPF", mensalista.CPF);
        cmd.Parameters.AddWithValue("Nome", mensalista.Nome);
        cmd.Parameters.AddWithValue("Placa", mensalista.Placa);
        cmd.Parameters.AddWithValue("TipoVeiculo", mensalista.TipoVeiculo);
        cmd.Parameters.AddWithValue("Descricao", mensalista.Descricao);
        cmd.Parameters.AddWithValue("DataEntrada", DateTime.Now);
        cmd.Parameters.AddWithValue("Valor", mensalista.Valor);
        cmd.Parameters.AddWithValue("Plano", mensalista.Plano);
        cmd.Parameters.AddWithValue("Situacao", mensalista.Situacao);
        cmd.ExecuteNonQuery();
      }
    }

    public List<Models.MensalistaPFModel> ListarMensalistasPF(SqlConnection _sql)
    {
      var listaClientes = new List<Models.MensalistaPFModel>();
      _sql.Open();

      string query = "Select CPF, Nome, Placa, Descricao, TipoVeiculo, Valor, Plano, DataEntrada, Situacao  From MensalistaPF;";

      using (var cmd = new SqlCommand(query, _sql))
      {

        var rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
          var cliente = new Models.MensalistaPFModel();
          cliente.CPF = Convert.ToString(rdr["CPF"]);
          cliente.Nome = Convert.ToString(rdr["Nome"]);
          cliente.Placa = Convert.ToString(rdr["Placa"]);
          cliente.TipoVeiculo = Convert.ToString(rdr["TipoVeiculo"]);
          cliente.Descricao = Convert.ToString(rdr["Descricao"]);
          cliente.Plano = Convert.ToInt32(rdr["Plano"]);
          cliente.Situacao = Convert.ToBoolean(rdr["Situacao"]);
          cliente.Valor = Convert.ToDecimal(rdr["Valor"]);
          cliente.DataEntrada = Convert.ToDateTime(rdr["DataEntrada"]);

          listaClientes.Add(cliente);

        }
      }
      return listaClientes;
    }
  }
}
