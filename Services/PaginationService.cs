using CarteiraClientes.Infrastructure.Mappling;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.ClientCompany;
using CarteiraClientes.ViewModels.Company;
using cloudscribe.Pagination.Models;

namespace CarteiraClientes.Services;

public class PaginationService : IPaginationService
{
    private readonly AppDbContext _dbContext;

    public PaginationService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<PagedResult<ClientResultViewModel>>> PagingClientsAsync(string searchString,
        string sortOrder,
        int pageNumber = 1,
        int pageSize = 15)
    {
        var serviceResponse = new ServiceResponse<PagedResult<ClientResultViewModel>>();

        var excludeRecords = pageSize * pageNumber - pageSize;
        var clientsQuery = _dbContext.Clients
            .AsNoTracking();

        // Verify if seacrhString is null or empty 
        if (!string.IsNullOrEmpty(searchString))
            clientsQuery = clientsQuery.Where(c => c.FullName.Contains(searchString));

        // Sort Logic
        switch (sortOrder)
        {
            case "name_desc":
                clientsQuery = clientsQuery.OrderByDescending(c => c.FullName);
                break;
            case "is_overdue":
                clientsQuery = clientsQuery.OrderByDescending(c => c.IsOverdue);
                break;
            default:
                clientsQuery = clientsQuery.OrderBy(c => c.FullName);
                break;
        }

        // Pagination
        var pagedClients = await clientsQuery
            .Skip(excludeRecords)
            .Take(pageSize)
            .ToListAsync();

        var totalItems = await clientsQuery.CountAsync();

        var clientViewModels = pagedClients.Select(x => ClientMapper.ClientToClientResultViewModel(x)).ToList();

        var result = new PagedResult<ClientResultViewModel> // cloudscribe.Pagination.Models to help at view
        {
            Data = clientViewModels,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        serviceResponse.Data = result;

        return serviceResponse;
    }

    public async Task<ServiceResponse<PagedResult<CompanyResultViewModel>>> PagingCompanyAsync(int pageNumber = 1,
        int pageSize = 15)
    {
        var serviceResponse = new ServiceResponse<PagedResult<CompanyResultViewModel>>();

        var excludedRecords = pageSize * pageNumber - pageSize;
        var query = _dbContext.Companies
            .AsNoTracking()
            .OrderBy(c => c.CompanyId);

        var pagedCompanies = await query
            .Skip(excludedRecords)
            .Take(pageSize)
            .ToListAsync();

        var totalItems = await query.CountAsync();

        var companyViewModel = pagedCompanies.Select(x => CompanyMapper.CompanyToCompanyResultViewModel(x)).ToList();

        var result = new PagedResult<CompanyResultViewModel> // cloudscribe.Pagination.Models to help at view
        {
            Data = companyViewModel,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        serviceResponse.Data = result;

        return serviceResponse;
    }

    public async Task<ServiceResponse<PagedResult<ClientCompanyResultViewModel>>> PagingClientCompanyAsync(
        int pageNumber = 1,
        int pageSize = 15)
    {
        var serviceResponse = new ServiceResponse<PagedResult<ClientCompanyResultViewModel>>();

        var excludedRecords = pageSize * pageNumber - pageSize;
        var clientsCompaniesQuery = (from cl in _dbContext.Clients
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
            }).AsNoTracking();

        var pagedClientsCompanies = await clientsCompaniesQuery
            .OrderBy(cc => cc.ClientId)
            .Skip(excludedRecords)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        var totalItems = await clientsCompaniesQuery.CountAsync();

        var clientCompanyModel = pagedClientsCompanies.Select(x => new ClientCompanyResultViewModel
        {
            ClientId = x.ClientId,
            FullName = x.FullName,
            Document = x.Document,
            IsOverdue = x.IsOverdue,
            CompanyId = x.CompanyId,
            CompanyName = x.CompanyName
        }).ToList();

        var result = new PagedResult<ClientCompanyResultViewModel> // cloudscribe.Pagination.Models to help at view
        {
            Data = clientCompanyModel,
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        serviceResponse.Data = result;

        return serviceResponse;
    }
}