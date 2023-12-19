using CarteiraClientes.ViewModels.ClientCompany;
using CarteiraClientes.ViewModels.Report;

namespace CarteiraClientes.Interfaces;

public interface IReportRepository
{
    Task<ServiceResponse<List<GetReportViewModel>>> GetAllClientsCompaniesEfCoreRawQueryUnmappedTypeAsync();
}