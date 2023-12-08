using System.Data;
using CarteiraClientes.ViewModels.ClientCompany;
using Dapper;

namespace CarteiraClientes.Infrastructure.Repository;

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

    /// <summary>
    /// Performs an Inner Join operation with RawQuery for Unmapped Type
    /// </summary>
    /// <returns>Should return same data as traditional EF Core approach</returns>
    public async Task<ServiceResponse<List<GetClientCompanyViewModel>>> GetAllClientsCompaniesEFCoreRawQueryUnmappedType()
    {
        var serviceResponse = new ServiceResponse<List<GetClientCompanyViewModel>>();

        var clientsCompanies = await _dbContext
            .Database
            .SqlQueryRaw<GetClientCompanyViewModel>(
                """
                   SELECT c0.client_id AS ClientId,
                          c0.full_name AS FullName,
                          c0.age AS Age,
                          c0.document AS Document,
                          c0.gender AS Gender,
                          c0.is_overdue AS IsOverdue,
                   	   c1.company_id AS CompanyId,
                   	   c1.company_name AS CompanyName,
                   	   c1.founded_date AS FoundedDate,
                   	   c1.revenue AS Revenue
                   FROM clients AS c0
                   INNER JOIN clients_companies cc
                   	ON c0.client_id = cc.client_id
                   INNER JOIN companies c1
                   	ON c1.company_id = cc.company_id
                """
            ).AsNoTracking()
            .ToListAsync();

        serviceResponse.Data = clientsCompanies;

        return serviceResponse;
    }
}