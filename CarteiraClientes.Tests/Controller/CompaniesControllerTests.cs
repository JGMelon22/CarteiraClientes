using CarteiraClientes.Controllers;
using CarteiraClientes.Interfaces;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Company;
using cloudscribe.Pagination.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Tests.Controller;

public class CompaniesControllerTests
{
    private readonly CompaniesController _companiesController;
    private readonly ICompanyRepository _companyRepository;
    private readonly IPaginationService _paginationService;
    private readonly IValidator<AddCompanyViewModel> _addCompanyValidator;
    private readonly IValidator<UpdateCompanyViewModel> _updateCompanyValidator;

    public CompaniesControllerTests()
    {
        _companyRepository = A.Fake<ICompanyRepository>();
        _paginationService = A.Fake<IPaginationService>();
        _addCompanyValidator = A.Fake<IValidator<AddCompanyViewModel>>();
        _updateCompanyValidator = A.Fake<IValidator<UpdateCompanyViewModel>>();

        // SUT
        _companiesController = new CompaniesController(_companyRepository, _paginationService, _addCompanyValidator,
            _updateCompanyValidator);
    }

    [Fact]
    public void CompaniesController_Index_ReturnsSuccess()
    {
        // Arrange
        var companies = A.Fake<ServiceResponse<List<GetCompanyViewModel>>>();
        A.CallTo(() => _companyRepository.GetAllCompanies()).Returns(companies);

        // Act
        var result = _companiesController.Index();

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void CompaniesController_IndexPaged_ReturnsSuccess()
    {
        // Arrange
        int pageNumber = 1;
        int pageSize = 15;

        var companies = A.Fake<ServiceResponse<PagedResult<GetCompanyViewModel>>>();
        A.CallTo(() => _paginationService.PagingCompany(pageNumber, pageSize)).Returns(companies);

        // Act
        var result = _companiesController.IndexPaged();

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    public void CompaniesController_Details_ReturnsSuccess()
    {
        // Arrange
        int id = 1;
        var company = A.Fake<ServiceResponse<GetCompanyViewModel>>();
        A.CallTo(() => _companyRepository.GetCompanyByIdCompiledEfCoreQueryAsync(id)).Returns(company);

        // Act
        var result = _companiesController.Details(id);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void CompaniesController_Create_ReturnsSuccess()
    {
        // Arrange
        var newCompany = A.Fake<AddCompanyViewModel>();
        A.CallTo(() => _companyRepository.AddCompany(newCompany)).Returns(Task.CompletedTask);

        // Act 
        var result = _companiesController.Create(newCompany);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void CompaniesController_Edit_ReturnsSuccess()
    {
        // Arrange 
        var updatedCompany = A.Fake<UpdateCompanyViewModel>();
        var company = A.Fake<ServiceResponse<GetCompanyViewModel>>();

        A.CallTo(() => _companyRepository.UpdateCompany(updatedCompany)).Returns(company);

        // Act 
        var result = _companiesController.Edit(updatedCompany);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
        result.Should().NotBeNull();
    }

    [Fact]
    public void CompaniesController_DeleteConfirmed_ReturnsSuccess()
    {
        // Arrange
        int id = 1;
        A.CallTo(() => _companyRepository.RemoveCompany(id)).Returns(Task.CompletedTask);

        // Act
        var result = _companiesController.DeleteConfirmed(id);

        // Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }
}