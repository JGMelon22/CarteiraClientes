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

    [HttpGet]
    public async Task<IActionResult> Download(string reportName)
    {
        byte[] bytes = await _service.DownloadReport(reportName); // bytes array to invoke file download service
        return File(bytes, "application/octet-stream", reportName); // generic binary file type
    }

    [HttpGet]
    public IActionResult Delete(string reportName)
    {
        _service.DeleteReport(reportName);
        return RedirectToAction("Index", "Reports");
    }
}