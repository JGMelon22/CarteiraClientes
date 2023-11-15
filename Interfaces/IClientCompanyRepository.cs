using CarteiraClientes.ViewModels.Client;

namespace CarteiraClientes.Interfaces;

public interface IClientCompanyRepository
{
    Task<ServiceResponse<List<GetClientViewModel>>> GetAllClientsCompanies();
}