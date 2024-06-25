using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Infrastructure.Repository;

public class ClientCompanyRepository : IClientCompanyRepository
{
    private readonly AppDbContext _dbContext;

    public ClientCompanyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<List<ClientCompanyResultViewModel>>> GetAllClientsCompaniesAsync()
    {
        var serviceResponse = new ServiceResponse<List<ClientCompanyResultViewModel>>();

        var clientsCompanies = await (from cl in _dbContext.Clients
                join cc in _dbContext.ClientsCompanies on cl.ClientId equals cc.ClientId
                join co in _dbContext.Companies on cc.CompanyId equals co.CompanyId
                select new ClientCompanyResultViewModel()
                {
                    ClientId = cl.ClientId,
                    FullName = cl.FullName,
                    Document = cl.Document,
                    IsOverdue = cl.IsOverdue,
                    CompanyId = co.CompanyId,
                    CompanyName = co.CompanyName
                })
            .Take(100)
            .ToListAsync();

        // Manually Mapping
        serviceResponse.Data = clientsCompanies;

        return serviceResponse;
    }
}