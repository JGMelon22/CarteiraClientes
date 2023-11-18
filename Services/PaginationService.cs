using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Client;
using Mapster;

namespace CarteiraClientes.Services;

public class PaginationService : IPaginationService
{
    private readonly AppDbContext _dbContext;

    public PaginationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<List<GetClientViewModel>>> PagingClients(int pageNumber = 1, int pageSize = 1)
    {
        var serviceResponse = new ServiceResponse<List<GetClientViewModel>>();
    
        var excludeRecords = (pageSize * pageNumber) - pageSize;
        var clients = await _dbContext.Clients
            .Skip(excludeRecords)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        serviceResponse.Data = clients.Adapt<List<GetClientViewModel>>();

        return serviceResponse;
    }
}