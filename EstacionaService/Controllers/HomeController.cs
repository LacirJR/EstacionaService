using EstacionaService.Models;
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
