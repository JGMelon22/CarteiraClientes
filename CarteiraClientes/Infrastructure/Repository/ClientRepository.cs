using System.Data;
using CarteiraClientes.Infrastructure.Mappling;
using CarteiraClientes.ViewModels.Client;
using Dapper;

namespace CarteiraClientes.Infrastructure.Repository;

public class ClientRepository : IClientRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly AppDbContext _dbContext;

    public ClientRepository(AppDbContext dbContext, IDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;
    }

    //  Busca com Dapper para evitar memory overhead 
    public async Task<ServiceResponse<List<ClientResultViewModel>>> GetAllClientsAsync()
    {
        var serviceResponse = new ServiceResponse<List<ClientResultViewModel>>();
        var getAllClientsAsyncQuery = """
                                      select client_id as ClientId,
                                             full_name as FullName,
                                             age as Age,
                                             document as Document,
                                             gender as Gender,
                                             is_overdue as IsOverdue
                                      from clients
                                      order by client_id asc
                                      limit 100;
                                      """;

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<Client>(getAllClientsAsyncQuery);

        serviceResponse.Data = result.Select(ClientMapper.ClientToClientResultViewModel).ToList();

        _dbConnection.Close();

        return serviceResponse;
    }

    public async Task<ServiceResponse<ClientResultViewModel>> GetClientByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<ClientResultViewModel>();

        try
        {
            var client = await _dbContext.Clients.FindAsync(id);

            if (client == null)
                throw new Exception("Cliente not found!");

            serviceResponse.Data =
                ClientMapper.ClientToClientResultViewModel(client);
        }
        catch (Exception ex) // Por isso criamos uma service response. Facilita lidar com exceptions
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task AddClientAsync(ClientInputViewModel newClientInput)
    {
        var client = ClientMapper.ClientToClientInputViewModel(newClientInput);

        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<ClientResultViewModel>> UpdateClientAsync(int id,
        ClientInputViewModel updatedClient)
    {
        var serviceResponse = new ServiceResponse<ClientResultViewModel>();

        try
        {
            var client = await _dbContext.Clients.FindAsync(id);

            if (client == null) throw new Exception("Client not found!");

            ClientMapper.ApplyUpdate(updatedClient, client);

            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = ClientMapper.ClientToClientResultViewModel(client);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> RemoveClientAsync(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        try
        {
            var client = await _dbContext.Clients.FindAsync(id);

            if (client == null)
                throw new Exception("Client not found!");

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }

        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}