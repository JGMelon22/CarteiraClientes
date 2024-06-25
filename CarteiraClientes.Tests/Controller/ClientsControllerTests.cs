using CarteiraClientes.Controllers;
using CarteiraClientes.Interfaces;
using CarteiraClientes.Models;
using CarteiraClientes.Models.Enums;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.ClientCompany;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Tests.Controller;

public class ClientsControllerTests
{
    private readonly ClientsController _controller;
    private readonly IClientRepository _repository;

    public ClientsControllerTests()
    {
        var clientInputValidator = A.Fake<IValidator<ClientInputViewModel>>();
        var pagination = A.Fake<IPaginationService>();
        _repository = A.Fake<IClientRepository>();

        // SUT
        _controller = new ClientsController(pagination, _repository, clientInputValidator);
    }

    [Fact]
    [Trait("ClientsController", "AddClientAsync")]
    public void ClientsController_AddNewClientAsync_ReturnsClients()
    {
        var newClient = new ClientInputViewModel
        {
            FullName = "New Client",
            Age = 19,
            Document = "987654321",
            Gender = Gender.Male,
            IsOverdue = false,
            ClientsCompanies = new List<AddClientAsyncCompanyViewModel>
            {
                new(11)
            }
        };
        A.CallTo(() => _repository.AddClientAsync(newClient)).Returns(Task.CompletedTask);

        var result = _controller.Create(newClient);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ClientsController", "GetAllClientsAsync")]
    public void ClientsController_GetAllClientsAsync_ReturnsClients()
    {
        var clients = A.Fake<ServiceResponse<List<ClientResultViewModel>>>();
        A.CallTo(() => _repository.GetAllClientsAsync()).Returns(clients);

        var result = _controller.Index();

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ClientsController", "GetClientByIdAsync")]
    public void ClientsController_GetClientByIdAsync_ReturnsClient()
    {
        var id = 1;
        var client = A.Fake<ServiceResponse<ClientResultViewModel>>();
        A.CallTo(() => _repository.GetClientByIdAsync(id)).Returns(client);

        var result = _controller.Details(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ClientsController", "UpdateClientAsync")]
    public void ClientsController_UpdateClientAsync_ReturnsClient()
    {
        var id = 1;
        var updatedClient = new ClientInputViewModel
        {
            FullName = "Updated Client",
            Age = 18,
            Document = "123456789",
            Gender = Gender.Female,
            IsOverdue = false,
            ClientsCompanies = new List<AddClientAsyncCompanyViewModel>
            {
                new(10)
            }
        };
        var clientResult = A.Fake<ServiceResponse<ClientResultViewModel>>();
        A.CallTo(() => _repository.UpdateClientAsync(id, updatedClient)).Returns(clientResult);

        var result = _controller.Edit(id, updatedClient);
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("ClientsController", "RemoveClientAsync")]
    public async Task ClientsController_RemoveClientAsync_ReturnsClients()
    {
        // Arrange
        var id = 1;
        var serviceResponse = new ServiceResponse<bool>();

        A.CallTo(() => _repository.RemoveClientAsync(id)).Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await _controller.DeleteConfirmed(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<RedirectToActionResult>();
        serviceResponse.Success.Should().BeTrue();
    }
}