﻿using Microsoft.Extensions.Configuration;
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
        cmd.Parameters.AddWithValue("Descricao", cliente.Descricao);
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
      string query = "SELECT ID, Descricao, Placa, TipoVeiculo, Entrada, Saida, ValorAPagar, Situacao FROM ClientesAtivosPatio WHERE ID = @id " +
          "Update ClientesAtivosPatio SET Situacao = 0 Where ID = @id";

      using (var cmd = new SqlCommand(query, _sql))
      {
        cmd.Parameters.AddWithValue("@ID", id);
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
                                          " ValorPago," +
                                          " DataHora," +
                                          " Situacao)" +

                             "VALUES      (@ID," +
                                         "  @Placa," +
                                          " @TipoVeiculo," +
                                          " @TempoGasto," +
                                          " @Valor," +
                                          " @DataHora," +
                                          " @Situacao) ";

      query += " UPDATE Cliente SET Saida = @Saida, ValorAPagar = @ValorAPagar, TempoGasto = @TempoGasto Where ID = @id";

      using (SqlCommand cmd = new SqlCommand(query, _sql))
      {

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
  }
}
