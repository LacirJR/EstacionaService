using EstacionaService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionaService.Controllers
{

    public class ClientesController : ControllerBase
    {
        private ClientesService _service;
        public ClientesController()
        {
            _service = new ClientesService();
        }

        [HttpPost("api/InserirCliente")]
        public IActionResult InserirCliente([FromBody] Models.ClienteModel cliente)
        {
            try
            { 

             cliente.Saida = Convert.ToDateTime("00:00");
            _service.InserirCliente(cliente);

                return StatusCode(200, "Sucesso ao inserir");
            }
            catch (InvalidOperationException)
            {
                return StatusCode(400, "Erro ao inserir");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao conectar ao servidor: "+ ex.Message);
            }
        }

        [HttpGet("api/ListarClientes")]
        public IActionResult ListarClientes()
        {
            try
            {
                var listaClientes = _service.ListarClientes();

                var lista = listaClientes.Select(x => new
                {
                    x.ID,
                    x.Descricao,
                    x.Placa,
                    x.TipoVeiculo,
                    x.Entrada

                }).ToList();

                return StatusCode(200, lista);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao realizar a busca");
            }
        }
        [HttpPut("api/FecharValor/{id}")]
        public IActionResult FechamentoCliente(string id)
        {
            try
            {
                var clienteFechamento = _service.FechamentoCliente(id);
                return StatusCode(200, clienteFechamento);
            }
            catch (Exception EX)
            {
                return StatusCode(500, "Erro ao realizar o fechamento: " + EX.Message);
            }
        }

        [HttpPut("api/Pagamento")]
        public IActionResult Pagamento(decimal valorPago, decimal valorAReceber, string placa)
        {
            try
            {
                var pagamento = _service.Pagamento(valorPago, valorAReceber,placa);
                return StatusCode(200, pagamento);
            }
            catch (Exception EX)
            {
                return StatusCode(500, "Erro ao realizar o pagamento: " + EX.Message);
            }
        }
    }
}
