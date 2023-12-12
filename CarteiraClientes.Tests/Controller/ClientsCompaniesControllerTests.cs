using CarteiraClientes.Controllers;
using CarteiraClientes.Interfaces;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.ClientCompany;
using cloudscribe.Pagination.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Tests.Controller;

public class ClientsCompaniesControllerTests
{
    private readonly ClientsCompaniesController _clientsCompaniesController;
    private readonly IClientCompanyRepository _clientCompanyRepository;
    private readonly IPaginationService _paginationService;

    public ClientsCompaniesControllerTests()
    {
        _clientCompanyRepository = A.Fake<IClientCompanyRepository>();
        _paginationService = A.Fake<IPaginationService>();

        // SUT
        _clientsCompaniesController = new ClientsCompaniesController(_clientCompanyRepository, _paginationService);
    }

    [Fact]
    public void ClientsCompaniesController_Index_ReturnSuccess()
    {
        // Arrange 
        var clientsCompanies = A.Fake<ServiceResponse<List<GetClientCompanyViewModel>>>();
        A.CallTo(() => _clientCompanyRepository.GetAllClientsCompanies()).Returns(clientsCompanies);

        // Act
        var result = _clientsCompaniesController.Index();

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void ClientsCompaniesController_IndexPaged_ReturnSuccess()
    {
        // Arrange
        int pageNumber = 1;
        int pageSize = 15;

        var clientsCompanies = A.Fake<ServiceResponse<PagedResult<GetClientCompanyViewModel>>>();
        A.CallTo(() => _paginationService.PagingClientCompany(pageNumber, pageSize))
            .Returns(clientsCompanies);

        // Act
        var result = _clientsCompaniesController.IndexPaged(pageNumber, pageSize);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }
}