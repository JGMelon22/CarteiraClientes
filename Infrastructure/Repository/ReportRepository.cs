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
            .SqlQueryRaw<GetReportViewModel>(@"
                   SELECT c.client_id AS ClientId, 
                          c.full_name AS FullName, 
                          c.document AS Document, 
                          c.is_overdue AS IsOverdue, 
                          c1.company_id AS CompanyId, 
                          c1.company_name AS CompanyName
                   FROM clients AS c
                   INNER JOIN clients_companies AS c0 
                   	ON c.client_id = c0.client_id
                   INNER JOIN companies AS c1 
                   	ON c0.company_id = c1.company_id;
                "
            ).AsNoTracking()
            .ToListAsync();

        serviceResponse.Data = clientsCompanies;

        return serviceResponse;
    }
}