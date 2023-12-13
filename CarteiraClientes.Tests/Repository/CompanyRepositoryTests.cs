using System.Data;
using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Company;
using Microsoft.EntityFrameworkCore;

namespace CarteiraClientes.Tests.Repository;

public class CompanyRepositoryTests
{
    private readonly AppDbContext _dbContext;
    private readonly IDbConnection _dbConnection;
    private readonly CompanyRepository _companyRepository;

    public CompanyRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        if (_dbContext.Companies.Count() < 0)
        {
            for (int i = 0; i < 10; i++)
            {
                _dbContext.Companies.Add(
                    new Company()
                    {
                        CompanyName = "New Fake Company",
                        FoundedDate = new DateTime(2010, 07, 22),
                        Revenue = 123456789.0M
                    }
                );

                _dbContext.SaveChanges();
            }

            _dbConnection = A.Fake<IDbConnection>(); // Create a fake IDbConnection using FakeItEasy

            _companyRepository = new CompanyRepository(_dbContext, _dbConnection);
        }
    }

    [Fact]
    public void CompanyRepository_AddCompany_ReturnsSuccess()
    {
        // Arrange
        AddCompanyViewModel newCompany = new AddCompanyViewModel()
        {
            CompanyName = "New Fake Company",
            FoundedDate = new DateTime(2010, 07, 22),
            Revenue = 123456789.0M
        };

        // Act
        var result = _companyRepository.AddCompany(newCompany);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void CompanyRepository_GetCompanyByIdCompiledEfCoreQueryAsync_ReturnsCompany()
    {
        // Arrange
        int id = 1;

        // Act 
        var result = _companyRepository.GetCompanyByIdCompiledEfCoreQueryAsync(id);

        // Assert
        result.Should().NotBeNull();
    }
}