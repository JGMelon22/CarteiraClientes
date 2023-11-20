using CarteiraClientes.ViewModels.Report;

namespace CarteiraClientes.Interfaces;

public interface IReportService
{
    List<ReportViewModel> GetAllReports();
    Task PlotReport();
    Task<byte[]> DownloadReport(string reportName);
    void DeleteReport(string reportName);
}