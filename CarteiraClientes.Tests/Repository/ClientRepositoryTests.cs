using System.Data;
using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Models;
using CarteiraClientes.Models.Enums;
using CarteiraClientes.ViewModels.Client;
using Microsoft.EntityFrameworkCore;

namespace CarteiraClientes.Tests.Repository;

public class ClientRepositoryTests
{
    private readonly IDbConnection _dbConnection;
    private readonly AppDbContext _dbContext;
    private readonly ClientRepository _repository;

    public ClientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        _dbConnection = A.Fake<IDbConnection>();
        _repository = new ClientRepository(_dbContext, _dbConnection);

        if (_dbContext.Clients.Count() == 0)
            for (var i = 0; i < 10; i++)
                _dbContext.Clients.Add(
                    new Client
                    {
                        FullName = $"New Fake Client {i}",
                        Age = 18,
                        Document = "123456789",
                        Gender = Gender.Female,
                        IsOverdue = false
                    });

        _dbContext.SaveChanges();
    }

    [Fact]
    [Trait("ClientRepository", "AddClientAsync")]
    public void ClientRepository_AddNewClientAsync()
    {
        var newClient = new ClientInputViewModel
        {
            FullName = "New Fake Client",
            Age = 19,
            Document = "987654321",
            Gender = Gender.Male,
            IsOverdue = true
        };

        var result = _repository.AddClientAsync(newClient);

        result.Should().NotBeNull();
    }

    [Fact]
    [Trait("ClientRepository", "GetAllClientsAsync")]
    public void ClientRepository_GetAllClientsAsync_ReturnsClients()
    {
        var result = _repository.GetAllClientsAsync();

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<List<ClientResultViewModel>>>>();
    }

    [Fact]
    [Trait("ClientRepository", "GetClientByIdAsync")]
    public void ClientRepository_GetClientByIdAsync_ReturnsClient()
    {
        var id = 1;

        var result = _repository.GetClientByIdAsync(id);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<ClientResultViewModel>>>();
    }

    [Fact]
    [Trait("ClientRepository", "AddClientAsync")]
    public void ClientRepository_UpdateClientAsync_ReturnsClient()
    {
        var id = 1;
        var updatedClient = new ClientInputViewModel
        {
            FullName = "Updated Fake Client",
            Age = 20,
            Document = "123987546",
            Gender = Gender.Female,
            IsOverdue = true
        };

        var result = _repository.UpdateClientAsync(id, updatedClient);

        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<ClientResultViewModel>>>();
    }

    [Fact]
    [Trait("ClientRepository", "RemoveClientAsync")]
    public async Task ClientRepository_RemoveClientAsync()
    {
        int id = 1;

        var result = await _repository.RemoveClientAsync(id);

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Should().BeOfType<ServiceResponse<bool>>();
    }
}