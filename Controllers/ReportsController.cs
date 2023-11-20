using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class ReportsController : Controller
{
    private readonly IReportService _service;

    public ReportsController(IReportService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var reports = _service.GetAllReports();
        return View(reports);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await _service.PlotReport();
        return RedirectToAction("Index", "Reports");
    }
}