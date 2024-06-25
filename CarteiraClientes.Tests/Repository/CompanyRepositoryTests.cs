using System.Data;
using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Company;
using Microsoft.EntityFrameworkCore;

namespace CarteiraClientes.Tests.Repository;

public class CompanyRepositoryTests
{
    private readonly IDbConnection _dbConnection;
    private readonly AppDbContext _dbContext;
    private readonly CompanyRepository _repository;

    public CompanyRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        _dbConnection = A.Fake<IDbConnection>();
        _repository = new CompanyRepository(_dbContext, _dbConnection);

        if (_dbContext.Companies.Count() == 0)
            for (var i = 0; i < 10; i++)
                _dbContext.Companies.Add(
                    new Company
                    {
                        CompanyName = $"New Fake Company {i}",
                        FoundedDate = new DateTime(1993, 08, 04),
                        Revenue = 201007
                    });

        _dbContext.SaveChanges();
    }

    [Fact]
    [Trait("CompanyRepository", "AddCompanyAsync")]
    public void CompanyRepository_AddNewCompanyAsync()
    {
        var newCompany = new CompanyInputViewModel
        {
            CompanyName = "New Fake Company",
            FoundedDate = new DateTime(2010, 07, 22),
            Revenue = 199308
        };

        var result = _repository.AddCompanyAsync(newCompany);

        result.Should().NotBeNull();
    }

    [Fact]
    [Trait("CompanyRepository", "GetAllCompaniesAsync")]
    public void CompanyRepository_GetAllCompaniesAsync_ReturnsCompanies()
    {
        var result = _repository.GetAllCompaniesAsync();

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<List<CompanyResultViewModel>>>>();
    }

    [Fact]
    [Trait("CompanyRepository", "GetCompanyByIdAsync")]
    public void CompanyRepository_GetCompanyByIdAsync_ReturnsCompany()
    {
        var id = 1;

        var result = _repository.GetCompanyByIdCompiledEfCoreQueryAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<CompanyResultViewModel>>>();
    }

    [Fact]
    [Trait("CompanyRepository", "AddCompanyAsync")]
    public void CompanyRepository_UpdateCompanyAsync_ReturnsCompany()
    {
        var id = 1;
        var updatedCompany = new CompanyInputViewModel
        {
            CompanyName = "Upated Fake Company",
            FoundedDate = new DateTime(2002, 05, 22),
            Revenue = 201007
        };

        var result = _repository.UpdateCompanyAsync(id, updatedCompany);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<CompanyResultViewModel>>>();
    }

    [Fact]
    [Trait("CompanyRepository", "RemoveCompanyAsync")]
    public async Task CompanyRepository_RemoveCompanyAsync()
    {
        // Arrange
        var id = 1;

        // Act
        var result = await _repository.RemoveCompanyAsync(id);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Should().BeOfType<ServiceResponse<bool>>();
    }
}