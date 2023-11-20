using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.Services;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _dbContext;

    public ReportRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetClientCompanyViewModel>> GetAllClientsCompanies()
    {
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

        return clientsCompanies.Adapt<List<GetClientCompanyViewModel>>();
    }
}