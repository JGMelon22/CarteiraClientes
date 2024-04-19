using CarteiraClientes.Controllers;
using CarteiraClientes.Interfaces;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Company;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraClientes.Tests.Controller;

public class CompaniesControllerTests
{
    private readonly CompaniesController _controller;
    private readonly ICompanyRepository _repository;

    public CompaniesControllerTests()
    {
        _repository = A.Fake<ICompanyRepository>();
        var pagination = A.Fake<IPaginationService>();
        var companyInputValidator = A.Fake<IValidator<CompanyInputViewModel>>();

        // SUT
        _controller = new CompaniesController(_repository, pagination, companyInputValidator);
    }

    [Fact]
    [Trait("CompaniesController", "AddNewCompanyAsync")]
    public void CompaniesController_AddNewCompanyAsync_ReturnsCompanies()
    {
        var newCompany = new CompanyInputViewModel
        {
            CompanyName = "New Company",
            FoundedDate = new DateTime(1971, 12, 19),
            Revenue = 6900420.99M
        };
        A.CallTo(() => _repository.AddCompanyAsync(newCompany)).Returns(Task.CompletedTask);

        var result = _controller.Create(newCompany);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("CompaniesController", "GetAllCompaniesAsync")]
    public void CompaniesController_GetAllCompaniesAsync_ReturnsCompanies()
    {
        var companies = A.Fake<ServiceResponse<List<CompanyResultViewModel>>>();
        A.CallTo(() => _repository.GetAllCompaniesAsync()).Returns(companies);

        var result = _controller.Index();

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("CompaniesController", "GetCompanyByIdAsync")]
    public void CompaniesController_GetCompanyByIdAsync_ReturnsCompany()
    {
        var id = 1;
        var company = A.Fake<ServiceResponse<CompanyResultViewModel>>();
        A.CallTo(() => _repository.GetCompanyByIdCompiledEfCoreQueryAsync(id)).Returns(company);

        var result = _controller.Details(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("CompaniesController", "UpdateCompanyAsync")]
    public void CompaniesController_UpdateCompanyAsync_ReturnsCompany()
    {
        var id = 1;
        var updatedCompany = new CompanyInputViewModel
        {
            CompanyName = "Updated Company",
            FoundedDate = new DateTime(1993, 08, 04),
            Revenue = 4200069.99M
        };
        var companyResult = A.Fake<ServiceResponse<CompanyResultViewModel>>();
        A.CallTo(() => _repository.UpdateCompanyAsync(id, updatedCompany)).Returns(companyResult);

        var result = _controller.Edit(id, updatedCompany);
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]
    [Trait("CompaniesController", "RemoveCompanyAsync")]
    public void CompaniesController_RemoveCompanyAsync_ReturnsCompanies()
    {
        var id = 1;
        A.CallTo(() => _repository.RemoveCompanyAsync(id)).Returns(Task.CompletedTask);

        var result = _controller.DeleteConfirmed(id);
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<IActionResult>>();
    }
}