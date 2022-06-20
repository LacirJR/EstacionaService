using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using EstacionaService.Data;
using System.Collections.Generic;

namespace EstacionaService.Service
{
  public class MensalistaService
  {
    private readonly SqlConnection _sql;
    public MensalistaService()
    {
      string stringConexao = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionString")["StringConexao"];
      var conexao = File.ReadAllText(stringConexao);
      _sql = new SqlConnection(conexao);
    }


    public void CadastrarMensalista(Models.MensalistaPFModel mensalista)
    {
      var sql = new ClientesData();
      sql.CadastrarMensalista(mensalista, _sql);
    }

    public List<Models.MensalistaPFModel> ListarMensalistasPF()
    {
      var sql = new Data.ClientesData();

      return sql.ListarMensalistasPF(_sql);
    }

  }
}