using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.ClientCompany;
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
        var clients = await _dbContext.Clients
            .Skip(excludeRecords)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        // Use Mapster to adapt clientEntities to List<GetClientViewModel>
        var clientViewModels = clients.Adapt<List<GetClientViewModel>>();

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
        var companies = await _dbContext.Companies
            .Skip(excludedRecords)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        // Use Mapster to adapt clientEntities to List<GetClientViewModel>
        var companyViewModel = companies.Adapt<List<GetCompanyViewModel>>();

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

    public async Task<ServiceResponse<PagedResult<GetClientCompanyViewModel>>> PagingClientCompany(int pageNumber = 1,
        int pageSize = 15)
    {
        var serviceResponse = new ServiceResponse<PagedResult<GetClientCompanyViewModel>>();

        var excludedRecords = pageSize * pageNumber - pageSize;
        var clientsCompanies = await (from cl in _dbContext.Clients
                join cc in _dbContext.ClientsCompanies on cl.ClientId equals cc.ClientId
                join co in _dbContext.Companies on cc.CompanyId equals co.CompanyId
                select new
                {
                    cl.ClientId,
                    cl.FullName,
                    cl.Document,
                    cl.IsOverdue,
                    co.CompanyId,
                    co.CompanyName
                }).Skip(excludedRecords)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        // Use Mapster to adapt clientEntities to List<GetClientViewModel>
        var clientCompanyModel = clientsCompanies.Adapt<List<GetClientCompanyViewModel>>();

        var result = new PagedResult<GetClientCompanyViewModel> // cloudscribe.Pagination.Models to help at view
        {
            Data = clientCompanyModel,
            TotalItems = clientsCompanies.Count,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        serviceResponse.Data = result;

        return serviceResponse;
    }
}