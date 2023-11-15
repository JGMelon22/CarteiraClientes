using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Interfaces;

public interface IClientCompanyRepository
{
    Task<ServiceResponse<List<GetClientCompanyViewModel>>> GetAllClientsCompanies();
}