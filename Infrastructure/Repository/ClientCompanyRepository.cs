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

        // Manually Mapping
        serviceResponse.Data = clientsCompanies.Select(x => new ClientCompanyResultViewModel
        {
            ClientId = x.ClientId,
            FullName = x.FullName,
            Document = x.Document,
            IsOverdue = x.IsOverdue,
            CompanyId = x.CompanyId,
            CompanyName = x.CompanyName
        }).ToList();

        return serviceResponse;
    }
}