using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class ClientsController : Controller
{
    private readonly IPaginationService _pagination;
    private readonly IClientRepository _repository;

    public ClientsController(IClientRepository repository, IPaginationService pagination)
    {
        _repository = repository;
        _pagination = pagination;
    }

    // Clientes Paginados
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var clients = await _repository.GetAllClients();
        return clients.Data != null
            ? View(clients.Data)
            : NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> IndexPaged(string sortOrder, int pageNumber = 1, int pageSize = 15)
    {
        ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewBag.IsOverdueSortParam = string.IsNullOrEmpty(sortOrder) ? "is_overdue" : "";
        var clients = await _pagination.PagingClients(sortOrder, pageNumber, pageSize);
        return clients.Data != null
            ? View(clients.Data)
            : NoContent();
    }


    // View Detalhes cliente selecionado
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var client = await _repository.GetClientById(id);
        return client.Data != null
            ? View(client.Data)
            : NotFound(client);
    }

    // View Adicionar novo cliente
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Ação criar novo cliente
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AddClientViewModel newClient)
    {
        if (!ModelState.IsValid)
            return View(nameof(Create));

        try
        {
            await _repository.AddClient(newClient);
        }
        catch (Exception e)
        {
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction(nameof(Index));
    }

    // Chamar View Editar
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var client = await _repository.GetClientById(id);
        return client != null
            ? View(client.Data)
            : NotFound(client);
    }

    // Ação editar cliente
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UpdateClientViewModel updatedClient)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var client = await _repository.UpdateClient(updatedClient);
        return client.Data != null
            ? RedirectToAction(nameof(Index))
            : View(nameof(Edit));
    }

    // Chamar view deletar cliente
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var client = await _repository.GetClientById(id);
        return client.Data != null
            ? View(client.Data)
            : NotFound(client);
    }

    // Action de deletar cliente
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _repository.RemoveClient(id);
        return RedirectToAction(nameof(Index));
    }
}