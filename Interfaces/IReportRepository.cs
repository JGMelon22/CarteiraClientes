using CarteiraClientes.ViewModels.Report;

namespace CarteiraClientes.Interfaces;

public interface IReportRepository
{
    Task<ServiceResponse<List<ReportResultViewModel>>> GetAllClientsCompaniesEfCoreRawQueryUnmappedTypeAsync();
}