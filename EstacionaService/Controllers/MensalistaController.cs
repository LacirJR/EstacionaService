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
        cpf = x.CPF,
        nome = x.Nome,
        placa = x.Placa,
        tipoVeiculo = x.TipoVeiculo,
        descricao = x.Descricao,
        situacao = x.Situacao == false ? "Inativo" : "Ativo",
        action = $"<button class=\"btn btn-warning\">Teste</button>"
      }).ToList();

      return StatusCode(200, new { data = data });
    }


  }
}