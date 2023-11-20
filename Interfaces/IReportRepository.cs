using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Interfaces;

public interface IReportRepository
{
    Task<List<GetClientCompanyViewModel>> GetAllClientsCompanies();
}