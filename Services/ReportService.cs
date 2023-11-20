using System.Data;
using CarteiraClientes.ViewModels.Report;
using ClosedXML.Excel;

namespace CarteiraClientes.Services;

public class ReportService : IReportService
{
    /// <summary>
    /// DI to have Clients X Companies query
    /// </summary>
    private readonly IReportRepository _repository;

    public ReportService(IReportRepository repository)
    {
        _repository = repository;
    }

    public List<ReportViewModel> GetAllReports()
    {
        // Current path where reports should exist
        var filePath = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Reports"));

        // Create a list with all found reports on directory
        var createdReports = new List<ReportViewModel>();

        // Adds to the list each report found 
        foreach (var report in filePath)
        {
            createdReports.Add(new ReportViewModel()
            {
                ReportName = Path.GetFileName(report)
            });
        }

        return createdReports.ToList();
    }

    public async Task PlotReport()
    {
        var dataTable = new DataTable();
        dataTable.Columns.AddRange(
            new DataColumn[6] // Six headers due to projection from GetClientsCompanies EF Core Query
            {
                new("Id Cliente"),
                new("Nome Completo"),
                new("Registro Geral"),
                new("Inadimplente?"),
                new("Id Empresa"),
                new("Nome Empresa")
            });

        // Query fetch data from clients x companies
        var results = await _repository.GetAllClientsCompanies();

        // Populate report with DB data
        foreach (var result in results)
        {
            dataTable.Rows.Add(result.ClientId, result.FullName, result.Document, result.IsOverdue, result.CompanyId,
                result.CompanyName);
        }

        // Directory where reports should be places
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports");

        // If directory does not exists, create one
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Create the report per se
        using var workBook = new XLWorkbook();
        workBook.Worksheets.Add(dataTable, "ClientesEmpresas");
        workBook.SaveAs(folderPath + Path.DirectorySeparatorChar + "RelatorioClientesEmpresas.xlsx");
    }

    public async Task<byte[]> DownloadReport(string reportName)
    {
        // Find file (report) path
        var path = Path.Combine(Path.Combine(Environment.CurrentDirectory,
            "Reports" + Path.DirectorySeparatorChar) + reportName);

        // Read file
        var bytes = await File.ReadAllBytesAsync(path);
        return bytes;
    }

    public void DeleteReport(string reportName)
    {
        // Find file (report) path
        var path = Path.Combine(Path.Combine(Environment.CurrentDirectory, "Reports" +
                                                                           Path.DirectorySeparatorChar) + reportName);
        // Will delete a report if does exists
        if (File.Exists(path))
            File.Delete(path);
    }
}