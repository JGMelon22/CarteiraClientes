using CarteiraClientes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientRepository _repository;

    public ClientsController(IClientRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clients = await _repository.GetAllClients();
        return clients.Data != null
            ? Ok(clients)
            : NoContent();
    }
}