using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Clase_11.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clase_11.Controllers;

[Authorize] // Tambien puede ser utilizado para el controlador en su totalidad, si o si tiene que logearse para poder usar el controlador
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize(Roles = "Administrador, Operador")]// digo que quiero que sea privada, solo entren los que estan autenticado
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
