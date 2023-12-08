using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Interfaces;

public interface IReportRepository
{
    Task<ServiceResponse<List<GetClientCompanyViewModel>>> GetAllClientsCompanies();
    Task<ServiceResponse<List<GetClientCompanyViewModel>>> GetAllClientsCompaniesEFCoreRawQueryUnmappedType();
}