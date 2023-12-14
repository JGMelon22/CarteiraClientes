using System.Data;
using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Infrastructure.Repository;
using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Client;
using Microsoft.EntityFrameworkCore;
using CarteiraClientes.Models.Enums;

namespace CarteiraClientes.Tests.Repository;

public class ClientRepositoryTests
{
    private readonly AppDbContext _dbContext;
    private readonly IDbConnection _dbConnection;
    private readonly ClientRepository _clientRepository;

    public ClientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);
        _dbContext.Database.EnsureCreated();

        _dbConnection = A.Fake<IDbConnection>();
        _clientRepository = new ClientRepository(_dbContext, _dbConnection);

        if (_dbContext.Clients.Count() == 0)
        {
            for (int i = 0 - 1; i < 10; i++)
            {
                _dbContext.Clients.Add(
                    new Client()
                    {
                        FullName = "New Fake Client Name",
                        Age = 18,
                        Document = "123456789",
                        Gender = Gender.Female,
                        IsOverdue = false
                    }
                );
            }

            _dbContext.SaveChanges();
        }
    }

    [Fact]
    public void ClientRepository_GetAllClients_ReturnsClients()
    {
        // Arrange

        // Assert
        var result = _clientRepository.GetAllClients();

        // Act
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<List<GetClientViewModel>>>>();
    }

    [Fact]
    public void ClientRepository_GetClientById_ReturnsClient()
    {
        // Arrange
        int id = 1;

        // Act
        var result = _clientRepository.GetClientById(id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<GetClientViewModel>>>();
    }

    [Fact]
    public void ClientRepository_AddClient_ReturnsSuccess()
    {
        // Assert
        AddClientViewModel newClient = new AddClientViewModel()
        {
            FullName = "New Fake Client Name",
            Age = 18,
            Document = "123456789",
            Gender = Gender.Female,
            IsOverdue = false
        };

        // Act 
        var result = _clientRepository.AddClient(newClient);

        // Arrange
        result.Should().NotBeNull();
    }

    [Fact]
    public void ClientRepository_UpdateClient_ReturnsClient()
    {
        // Arrange
        var updatedClient = A.Fake<UpdateClientViewModel>();

        // Act
        var result = _clientRepository.UpdateClient(updatedClient);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Task<ServiceResponse<GetClientViewModel>>>();
    }

    [Fact]
    public void ClientRepository_RemoveClient_ReturnsClient()
    {
        // Arrange
        int id = 1;

        // Act 
        var result = _clientRepository.RemoveClient(id);

        // Assert
        result.Should().NotBeNull();
    }
}