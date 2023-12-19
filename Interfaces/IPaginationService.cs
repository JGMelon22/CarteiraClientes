using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.ClientCompany;
using CarteiraClientes.ViewModels.Company;
using cloudscribe.Pagination.Models;

namespace CarteiraClientes.Interfaces;

public interface IPaginationService
{
    Task<ServiceResponse<PagedResult<GetClientViewModel>>> PagingClientsAsync(string searchString, string sortOrder,
        int pageNumber, int pageSize);

    Task<ServiceResponse<PagedResult<GetCompanyViewModel>>> PagingCompanyAsync(int pageNumber, int pageSize);
    Task<ServiceResponse<PagedResult<GetClientCompanyViewModel>>> PagingClientCompanyAsync(int pageNumber, int pageSize);
}