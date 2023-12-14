using CarteiraClientes.Controllers.ExtensionMethods;
using CarteiraClientes.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Controllers;

public class ClientsController : Controller
{
    private readonly IPaginationService _pagination;
    private readonly IClientRepository _repository;
    private readonly IValidator<AddClientViewModel> _addClientValidator;
    private readonly IValidator<UpdateClientViewModel> _updateClientValidator;

    public ClientsController(IClientRepository repository, IPaginationService pagination,
        IValidator<AddClientViewModel> addClientValidator, IValidator<UpdateClientViewModel> updateClientValidator)
    {
        _repository = repository;
        _pagination = pagination;
        _addClientValidator = addClientValidator;
        _updateClientValidator = updateClientValidator;
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
    public async Task<IActionResult> IndexPaged(string searchString, string sortOrder, int pageNumber = 1,
        int pageSize = 15)
    {
        ViewBag.CurrentSortOrder = sortOrder; // To allow us to store current sort order when we change pages
        ViewBag.CurrentFilter = searchString; // Allow us to preserver search filter values
        ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewBag.IsOverdueSortParam = string.IsNullOrEmpty(sortOrder) ? "is_overdue" : "";
        var clients = await _pagination.PagingClients(searchString, sortOrder, pageNumber, pageSize);
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
        var result = await _addClientValidator.ValidateAsync(newClient);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Create));
        }

        try
        {
            await _repository.AddClient(newClient);
        }
        catch (Exception)
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
        var result = await _updateClientValidator.ValidateAsync(updatedClient);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View(nameof(Edit));
        }

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