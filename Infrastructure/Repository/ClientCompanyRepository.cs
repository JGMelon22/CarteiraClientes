using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Infrastructure.Repository;

public class ClientCompanyRepository : IClientCompanyRepository
{
    private readonly AppDbContext _dbContext;

    public ClientCompanyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<List<GetClientCompanyViewModel>>> GetAllClientsCompaniesAsync()
    {
        var serviceResponse = new ServiceResponse<List<GetClientCompanyViewModel>>();

        var clientsCompanies = await (from cl in _dbContext.Clients
                join cc in _dbContext.ClientsCompanies on cl.ClientId equals cc.ClientId
                join co in _dbContext.Companies on cc.CompanyId equals co.CompanyId
                select new
                {
                    cl.ClientId,
                    cl.FullName,
                    cl.Document,
                    cl.IsOverdue,
                    co.CompanyId,
                    co.CompanyName
                })
            .AsNoTracking()
            .Take(100)
            .ToListAsync();

        serviceResponse.Data = clientsCompanies.Adapt<List<GetClientCompanyViewModel>>();
        return serviceResponse;
    }
}