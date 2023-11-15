using CarteiraClientes.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class ChartsController : Controller
{
    private readonly ChartService _service;

    public ChartsController(ChartService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var chart = await _service.GenerateChart();
        return View(chart);
    }
}