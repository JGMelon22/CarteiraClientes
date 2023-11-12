using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class ClientsController : Controller
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
            ? View(clients.Data)
            : NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Details(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var client = await _repository.GetClientById(id);
        return client.Data != null
            ? Ok(client)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddClientViewModel newClient)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _repository.AddClient(newClient);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Edit(UpdateClientViewModel updatedClient)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var client = await _repository.UpdateClient(updatedClient);
        return client.Data != null
            ? Ok(client)
            : NotFound(client);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _repository.RemoveClient(id);
        return NoContent();
    }
}