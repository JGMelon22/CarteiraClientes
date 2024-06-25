using CarteiraClientes.ViewModels.Client;

namespace CarteiraClientes.Interfaces;

public interface IClientRepository
{
    Task<ServiceResponse<List<ClientResultViewModel>>> GetAllClientsAsync();
    Task<ServiceResponse<ClientResultViewModel>> GetClientByIdAsync(int id);
    Task AddClientAsync(ClientInputViewModel newClientInput);
    Task<ServiceResponse<ClientResultViewModel>> UpdateClientAsync(int id, ClientInputViewModel updatedClient);
    Task<ServiceResponse<bool>> RemoveClientAsync(int id);
}