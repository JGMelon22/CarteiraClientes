using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Interfaces;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Client;

namespace CarteiraClientes.Infrastructure.Repository;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<ServiceResponse<List<GetClientViewModel>>> GetAllClients()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetClientViewModel>> GetClientById(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddClient(AddClientViewModel newClient)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetClientViewModel>> UpdateClient(UpdateClientViewModel updatedClient)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClient(int id)
    {
        throw new NotImplementedException();
    }
}