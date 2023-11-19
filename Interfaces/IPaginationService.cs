using CarteiraClientes.ViewModels.Client;
using cloudscribe.Pagination.Models;

namespace CarteiraClientes.Interfaces;

public interface IPaginationService
{
    Task<ServiceResponse<PagedResult<GetClientViewModel>>> PagingClients(int pageNumber = 1, int pageSize = 1);
}