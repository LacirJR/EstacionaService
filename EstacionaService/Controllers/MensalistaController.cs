using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionaService.Controllers
{
  public class MensalistaController : Controller
  {
    private readonly ILogger<MensalistaController> _logger;
    private Service.MensalistaService _service;

    public MensalistaController(ILogger<MensalistaController> logger)
    {
      _logger = logger;
      _service = new Service.MensalistaService();
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult ListarMensalistasPF()
    {
      var data = _service.ListarMensalistasPF().Select(x => new
      {
        cpf = "<p>" + x.CPF + "</p>",
        nome = "<p>" + x.Nome + "</p>",
        placa = "<p>" + x.Placa + "</p>",
        tipoVeiculo = "<p>" + x.TipoVeiculo + "</p>",
        descricao = "<p>" + x.Descricao + "</p>",
        situacao = x.Situacao == false ? "<p>Inativo</p>" : "<p>Ativo</p>",
        action = $"<a type=\"button\" class=\"btn btn-warning btn-sm\">Teste</a>"
      }).ToList();

      return StatusCode(200, new { data = data });
    }


  }
}