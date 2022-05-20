using EstacionaService.Models;
using EstacionaService.RegrasDeNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EstacionaService.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private Service.ClientesService _service;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
      _service = new Service.ClientesService();
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult ListarClientesAtivos()
    {
      var clientes = _service.ListarClientes();

      return StatusCode(200, clientes);
    }

    [HttpPost]
    public IActionResult InserirCliente([FromBody] ClienteModel cliente)
    {
      try
      {
        Validacao.ValidarEntradaVeiculo(cliente);
        _service.InserirCliente(cliente);

        return StatusCode(200);
      }
      catch (InvalidOperationException ex)
      {
        return StatusCode(400, ex.Message);
      }
    }

    [HttpPut("/")]
    public IActionResult FechamentoCliente([FromBody] string id)
    {
      var clienteFechado = _service.FechamentoCliente(id);

      return StatusCode(200, clienteFechado);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
