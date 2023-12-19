using CarteiraClientes.ViewModels.Client;

namespace CarteiraClientes.Interfaces;

public interface IClientRepository
{
    Task<ServiceResponse<List<GetClientViewModel>>> GetAllClientsAsync();
    Task<ServiceResponse<GetClientViewModel>> GetClientByIdAsync(int id);
    Task AddClientAsync(AddClientViewModel newClient);
    Task<ServiceResponse<GetClientViewModel>> UpdateClientAsync(UpdateClientViewModel updatedClient);
    Task RemoveClientAsync(int id);
}