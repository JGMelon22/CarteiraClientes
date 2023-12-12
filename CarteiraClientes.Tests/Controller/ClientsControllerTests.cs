using CarteiraClientes.Controllers;
using CarteiraClientes.Interfaces;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Client;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Tests.Controller;

public class ClientsControllerTests
{
    private readonly ClientsController _clientsController;
    private readonly IClientRepository _clientRepository;
    private readonly IPaginationService _paginationService;

    public ClientsControllerTests()
    {
        _clientRepository = A.Fake<IClientRepository>();
        _paginationService = A.Fake<IPaginationService>();

        // SUT
        _clientsController = new ClientsController(_clientRepository, _paginationService);
    }

    [Fact]
    public void ClientsController_Index_ReturnsSuccess()
    {
        // Arrange
        var clients = A.Fake<ServiceResponse<List<GetClientViewModel>>>();
        A.CallTo(() => _clientRepository.GetAllClients()).Returns(clients);

        // Act 
        var result = _clientsController.Index();

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void ClientsController_IndexPaged_ReturnsSuccess()
    {
        // Arrange
        string searchString = "abc";
        string sortOrder = "name_desc";
        int pageNumber = 1;
        int pageSize = 15;
        
        var clients = A.Fake<ServiceResponse<PagedResult<GetClientViewModel>>>();
        A.CallTo(() => _paginationService.PagingClients(searchString, sortOrder, pageNumber, pageSize))
            .Returns(clients);

        // Act 
        var result = _clientsController.IndexPaged(searchString, sortOrder, pageNumber, pageSize);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void ClientsController_Details_ReturnsSuccess()
    {
        // Arrange 
        int id = 1;
        var client = A.Fake<ServiceResponse<GetClientViewModel>>();
        A.CallTo(() => _clientRepository.GetClientById(id)).Returns(client);

        // Act 
        var result = _clientsController.Details(id);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void ClientsController_Create_ReturnsSuccess()
    {
        // Arrange
        var newClient = A.Fake<AddClientViewModel>();
        A.CallTo(() => _clientRepository.AddClient(newClient)).Returns(Task.CompletedTask);

        // Act
        var result = _clientsController.Create(newClient);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void ClientsController_Edit_ReturnsSuccess()
    {
        // Arrange
        var updatedClient = A.Fake<UpdateClientViewModel>();
        var client = A.Fake<ServiceResponse<GetClientViewModel>>();
        A.CallTo(() => _clientRepository.UpdateClient(updatedClient)).Returns(client);

        // Act 
        var result = _clientsController.Edit(updatedClient);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void ClientsController_DeleteConfirmed_ReturnsSuccess()
    {
        // Arrange
        int id = 1;
        A.CallTo(() => _clientRepository.RemoveClient(id)).Returns(Task.CompletedTask);

        // Act
        var result = _clientsController.DeleteConfirmed(id);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }
}