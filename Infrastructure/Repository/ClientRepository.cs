using System.Data;
using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Client;
using Dapper;
using Mapster;

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
    public async Task<ServiceResponse<List<GetClientViewModel>>> GetAllClients()
    {
        var serviceResponse = new ServiceResponse<List<GetClientViewModel>>();
        var getAllClientsQuery = @"select client_id as ClientId,
                                          full_name as FullName,
                                          age as Age,
                                          document as Document,
                                          gender as Gender,
                                          is_overdue as IsOverdue
                                   from clients;";

        _dbConnection.Open();

        // var result = await _dbConnection.QueryAsync<GetClientViewModel>(getAllClientsQuery);
        var result = await _dbConnection.QueryAsync<GetClientViewModel>(getAllClientsQuery);

        // serviceResponse.Data = result.Adapt<List<GetClientViewModel>>().ToList(); // Mapeando Model para ViewModel
        serviceResponse.Data = result.Adapt<List<GetClientViewModel>>().ToList();

        _dbConnection.Close();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetClientViewModel>> GetClientById(int id)
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

    public async Task AddClient(AddClientViewModel newClient)
    {
        var client = newClient.Adapt<Client>();

        await _dbContext.Clients.AddAsync(client);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<GetClientViewModel>> UpdateClient(UpdateClientViewModel updatedClient)
    {
        var serviceResponse = new ServiceResponse<GetClientViewModel>();

        try
        {
            var client = await _dbContext.Clients.FindAsync(updatedClient.ClientId);

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

    public async Task RemoveClient(int id)
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