using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.Company;
using cloudscribe.Pagination.Models;
using Mapster;

namespace CarteiraClientes.Services;

public class PaginationService : IPaginationService
{
    private readonly AppDbContext _dbContext;

    public PaginationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<PagedResult<GetClientViewModel>>> PagingClients(int pageNumber = 1,
        int pageSize = 15)
    {
        var serviceResponse = new ServiceResponse<PagedResult<GetClientViewModel>>();

        var excludeRecords = pageSize * pageNumber - pageSize;
        var clients = _dbContext.Clients
            .Skip(excludeRecords)
            .Take(pageSize);

        var clientEntities = await clients
            .AsNoTracking()
            .ToListAsync();

        // Use Mapster to adapt clientEntities to List<GetClientViewModel>
        var clientViewModels = clientEntities.Adapt<List<GetClientViewModel>>();

        var result = new PagedResult<GetClientViewModel> // cloudscribe.Pagination.Models to help at view
        {
            Data = clientViewModels,
            TotalItems = await _dbContext.Clients.CountAsync(),
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        serviceResponse.Data = result;

        return serviceResponse;
    }

    public async Task<ServiceResponse<PagedResult<GetCompanyViewModel>>> PagingCompany(int pageNumber, int pageSize)
    {
        var serviceResponse = new ServiceResponse<PagedResult<GetCompanyViewModel>>();

        var excludedRecords = pageSize * pageNumber - pageSize;
        var companies = _dbContext.Companies
            .Skip(excludedRecords)
            .Take(pageSize);

        var companiesEntity = await companies
            .AsNoTracking()
            .ToListAsync();

        // Use Mapster to adapt clientEntities to List<GetClientViewModel>
        var companyViewModel = companiesEntity.Adapt<List<GetCompanyViewModel>>();

        var result = new PagedResult<GetCompanyViewModel> // cloudscribe.Pagination.Models to help at view
        {
            Data = companyViewModel,
            TotalItems = await _dbContext.Companies.CountAsync(),
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        serviceResponse.Data = result;

        return serviceResponse;
    }
}