using CarteiraClientes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class ClientsCompaniesController : Controller
{
    private readonly IClientCompanyRepository _repository;

    public ClientsCompaniesController(IClientCompanyRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clientsCompanies = await _repository.GetAllClientsCompanies();
        return clientsCompanies.Data != null
            ? View(clientsCompanies.Data)
            : NoContent();
    }
}