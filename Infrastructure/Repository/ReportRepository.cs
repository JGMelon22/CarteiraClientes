using CarteiraClientes.ViewModels.Report;

namespace CarteiraClientes.Infrastructure.Repository;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _dbContext;

    public ReportRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    ///     Performs an Inner Join operation with RawQuery for Unmapped Type
    /// </summary>
    /// <returns>Returns same data as traditional EF Core approach</returns>
    public async Task<ServiceResponse<List<GetReportViewModel>>> GetAllClientsCompaniesEfCoreRawQueryUnmappedType()
    {
        var serviceResponse = new ServiceResponse<List<GetReportViewModel>>();

        var clientsCompanies = await _dbContext
            .Database
            .SqlQueryRaw<GetReportViewModel>("""
                                                select c.client_id as ClientId,
                                                       c.full_name as FullName,
                                                       c.document as Document,
                                                       c.is_overdue as IsOverdue,
                                                       c1.company_id as CompanyId,
                                                       c1.company_name as CompanyName
                                                from clients as c
                                                inner join clients_companies as c0
                                                	on c.client_id = c0.client_id
                                                inner join companies as c1
                                                	on c0.company_id = c1.company_id;
                                             """
            ).AsNoTracking()
            .ToListAsync();

        serviceResponse.Data = clientsCompanies;

        return serviceResponse;
    }
}