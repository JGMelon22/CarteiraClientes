using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Services;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _dbContext;

    public ReportRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<List<GetClientCompanyViewModel>>> GetAllClientsCompanies()
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
                }).AsNoTracking()
            .ToListAsync();

        serviceResponse.Data = clientsCompanies.Adapt<List<GetClientCompanyViewModel>>();
        return serviceResponse;
    }
}