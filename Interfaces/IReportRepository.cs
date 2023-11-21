using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Interfaces;

public interface IReportRepository
{
    Task<ServiceResponse<List<GetClientCompanyViewModel>>> GetAllClientsCompanies();
}