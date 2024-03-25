using CarteiraClientes.Controllers;
using CarteiraClientes.Interfaces;
using CarteiraClientes.Models;
using CarteiraClientes.Models.Enums;
using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.ClientCompany;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CarteiraClienyes.Tests.Controllers;

public class ClientsControllerTests
{
    private readonly ClientsController _controller;
    private readonly IValidator<ClientInputViewModel> _clientInputValidator;
    private readonly IPaginationService _pagination;
    private readonly IClientRepository _repository;

    public ClientsControllerTests()
    {
        _clientInputValidator = A.Fake<IValidator<ClientInputViewModel>>();
        _pagination = A.Fake<IPaginationService>();
        _repository = A.Fake<IClientRepository>();

        // SUT
        _controller = new ClientsController(_pagination, _repository, _clientInputValidator);
    }

    [Fact]
    public void ClientsController_AddNewClientAsync_ReturnsClients()
    {
        var newClient = new ClientInputViewModel
        {
            FullName = "Updated Client",
            Age = 18,
            Document = "123456789",
            Gender = Gender.Female,
            IsOverdue = false,
            ClientsCompanies = new List<AddClientAsyncCompanyViewModel>()
            {
                new AddClientAsyncCompanyViewModel(10)
            }
        };
        A.CallTo(() => _repository.AddClientAsync(newClient)).Returns(Task.CompletedTask);

        var result = _controller.Create(newClient);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }
    
    [Fact]
    public void ClientsController_GetAllClientsAsync_ReturnsClients()
    {
        var clients = A.Fake<ServiceResponse<List<ClientResultViewModel>>>();
        A.CallTo(() => _repository.GetAllClientsAsync()).Returns(clients);

        var result = _controller.Index();

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void ClientsController_GetClientByIdAsync_ReturnsClient()
    {
        int id = 1;
        var client = A.Fake<ServiceResponse<ClientResultViewModel>>();
        A.CallTo(() => _repository.GetClientByIdAsync(id)).Returns(client);

        var result = _controller.Details(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void ClientsController_UpdateClientAsync_ReturnsClient()
    {
        int id = 1;
        var updatedClient = new ClientInputViewModel
        {
            FullName = "Updated Client",
            Age = 18,
            Document = "123456789",
            Gender = Gender.Female,
            IsOverdue = false,
            ClientsCompanies = new List<AddClientAsyncCompanyViewModel>()
            {
                new AddClientAsyncCompanyViewModel(10)
            }
        };
        var clientResult = A.Fake<ServiceResponse<ClientResultViewModel>>();

        A.CallTo(() => _repository.UpdateClientAsync(id, updatedClient)).Returns(clientResult);

        var result = _controller.Edit(id, updatedClient);
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void ClientsController_RemoveClientAsync_ReturnsClients()
    {
        int id = 1;
        A.CallTo(() => _repository.RemoveClientAsync(id)).Returns(Task.CompletedTask);

        var result = _controller.Delete(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }
}