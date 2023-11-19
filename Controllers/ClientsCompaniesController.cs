using CarteiraClientes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class ClientsCompaniesController : Controller
{
    private readonly IPaginationService _pagination;
    private readonly IClientCompanyRepository _repository;

    public ClientsCompaniesController(IClientCompanyRepository repository, IPaginationService pagination)
    {
        _repository = repository;
        _pagination = pagination;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clientsCompanies = await _repository.GetAllClientsCompanies();
        return clientsCompanies.Data != null
            ? View(clientsCompanies.Data)
            : NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> IndexPaged(int pageNumber = 1, int pageSize = 15)
    {
        var clientsCompanies = await _pagination.PagingClientCompany(pageNumber, pageSize);
        return clientsCompanies.Data != null
            ? View(clientsCompanies.Data)
            : NoContent();
    }
}