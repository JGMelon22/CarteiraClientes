using CarteiraClientes.ViewModels.Client;

namespace CarteiraClientes.Interfaces;

public interface IClientRepository
{
    Task<ServiceResponse<List<GetClientViewModel>>> GetAllClients();
    Task<ServiceResponse<GetClientViewModel>> GetClientById(int id);
    Task AddClient(AddClientViewModel newClient);
    Task<ServiceResponse<GetClientViewModel>> UpdateClient(UpdateClientViewModel updatedClient);
    Task RemoveClient(int id);
}