using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using EstacionaService.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EstacionaService.Service
{
    public class MensalistaService
    {
        private readonly Ef.EstacionaServiceContext _sql;
        public MensalistaService()
        {
            _sql = new Ef.EstacionaServiceContext();
        }


        public void CadastrarMensalista(Models.MensalistaPFModel mensalista)
        {
            _sql.MensalistaPfs.Add(new Ef.MensalistaPf()
            {
                Nome = mensalista.Nome,
                Cpf = mensalista.CPF,
                DataEntrada = DateTime.Now,
                Descricao = mensalista.Descricao,
                Situacao = mensalista.Situacao,
                Placa = mensalista.Placa,
                Plano = mensalista.Plano,
                TipoVeiculo = mensalista.TipoVeiculo,
                Valor = mensalista.Valor
            });
            _sql.SaveChanges();
        }

        public List<Ef.MensalistaPf> ListarMensalistasPF()
        {
           return _sql.MensalistaPfs.Select(x => x).ToList();
                                     
            

        }

    }
}