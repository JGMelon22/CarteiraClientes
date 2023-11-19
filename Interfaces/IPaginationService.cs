using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.ClientCompany;
using CarteiraClientes.ViewModels.Company;
using cloudscribe.Pagination.Models;

namespace CarteiraClientes.Interfaces;

public interface IPaginationService
{
    Task<ServiceResponse<PagedResult<GetClientViewModel>>>
        PagingClients(string sortOrder, int pageNumber, int pageSize);

    Task<ServiceResponse<PagedResult<GetCompanyViewModel>>> PagingCompany(int pageNumber, int pageSize);
    Task<ServiceResponse<PagedResult<GetClientCompanyViewModel>>> PagingClientCompany(int pageNumber, int pageSize);
}