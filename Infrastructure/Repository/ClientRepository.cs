using System.Data;
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
    public async Task<ServiceResponse<List<GetClientViewModel>>> GetAllClientsAsync()
    {
        var serviceResponse = new ServiceResponse<List<GetClientViewModel>>();
        var GetAllClientsAsyncQuery = """
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

        var result = await _dbConnection.QueryAsync<GetClientViewModel>(GetAllClientsAsyncQuery);

        serviceResponse.Data = result.ToList();

        _dbConnection.Close();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetClientViewModel>> GetClientByIdAsync(int id)
    {
        var serviceResponse = new ServiceResponse<GetClientViewModel>();

        try
        {
            var client = await _dbContext.Clients.FindAsync(id);

            if (client == null)
                throw new Exception("Cliente not found!");

            serviceResponse.Data = client.Adapt<GetClientViewModel>();
        }
        catch (Exception ex) // Por isso criamos uma service response. Facilita lidar com exceptions
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task AddClientAsync(AddClientViewModel newClient)
    {
        var client = newClient.Adapt<Client>();

        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<GetClientViewModel>> UpdateClientAsync(int id, UpdateClientViewModel updatedClient)
    {
        var serviceResponse = new ServiceResponse<GetClientViewModel>();

        try
        {
            var client = await _dbContext.Clients.FindAsync(id);

            if (client == null) throw new Exception("Client not found!");

            client.Adapt<UpdateClientViewModel>();

            client.FullName = updatedClient.FullName;
            client.Age = updatedClient.Age;
            client.Document = updatedClient.Document;
            client.Gender = updatedClient.Gender;

            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = client.Adapt<GetClientViewModel>();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task RemoveClientAsync(int id)
    {
        var serviceResponse = new ServiceResponse<GetClientViewModel>();

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
    }
}