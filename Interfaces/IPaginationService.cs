using CarteiraClientes.ViewModels.Client;

namespace CarteiraClientes.Interfaces;

public interface IPaginationService
{
    Task<ServiceResponse<List<GetClientViewModel>>> PagingClients(int pageNumber = 1, int pageSize = 1);
}