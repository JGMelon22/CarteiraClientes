using CarteiraClientes.ViewModels.Report;

namespace CarteiraClientes.Interfaces;

public interface IReportService
{
    List<ReportFileViewModel> GetAllReports();
    Task PlotReportAsync();
    Task<byte[]> DownloadReportAsync(string reportName);
    void DeleteReport(string reportName);
}