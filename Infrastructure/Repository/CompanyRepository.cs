using System.Data;
using CarteiraClientes.Infrastructure.Mappling;
using CarteiraClientes.ViewModels.Company;
using Dapper;

namespace CarteiraClientes.Infrastructure.Repository;

public class CompanyRepository : ICompanyRepository
{
    private static readonly Func<AppDbContext, int, Task<CompanyResultViewModel?>> GetCompany =
        EF.CompileAsyncQuery((AppDbContext context, int id) =>
            context.Companies
                .Select(c => new CompanyResultViewModel
                {
                    CompanyId = c.CompanyId,
                    CompanyName = c.CompanyName,
                    FoundedDate = c.FoundedDate,
                    Revenue = c.Revenue
                })
                .FirstOrDefault(x => x.CompanyId == id));

    private readonly IDbConnection _dbConnection;
    private readonly AppDbContext _dbContext;

    public CompanyRepository(AppDbContext dbContext, IDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;
    }

    public async Task<ServiceResponse<List<CompanyResultViewModel>>> GetAllCompaniesAsync()
    {
        var serviceResponse = new ServiceResponse<List<CompanyResultViewModel>>();
        var GetAllCompaniesAsyncQuery = """
                                        select company_id as CompanyId,
                                               company_name as CompanyName,
                                               founded_date as FoundedDate,
                                               revenue as Revenue
                                        from companies
                                        order by company_id asc
                                        limit 100;
                                        """;

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<CompanyResultViewModel>(GetAllCompaniesAsyncQuery);
        serviceResponse.Data = result.ToList();

        _dbConnection.Close();

        return serviceResponse;
    }

    public async Task<ServiceResponse<CompanyResultViewModel>> GetCompanyByIdCompiledEfCoreQueryAsync(int id)
    {
        var serviceResponse = new ServiceResponse<CompanyResultViewModel>();

        try
        {
            var company = await GetCompany(_dbContext, id);

            if (company == null) throw new Exception("Company not found!");

            serviceResponse.Data = company;
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task AddCompanyAsync(CompanyInputViewModel newCompanyInput)
    {
        var company = newCompanyInput.Adapt<Company>();

        await _dbContext.Companies.AddAsync(company);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<CompanyResultViewModel>> UpdateCompanyAsync(int id,
        CompanyInputViewModel updatedCompany)
    {
        var serviceResponse = new ServiceResponse<CompanyResultViewModel>();

        try
        {
            var company = await _dbContext.Companies.FindAsync(id);

            if (company == null) throw new Exception("Company not found!");
            updatedCompany.Adapt(company);

            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = company.Adapt<CompanyResultViewModel>();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task RemoveCompanyAsync(int id)
    {
        var serviceResponse = new ServiceResponse<CompanyResultViewModel>();

        try
        {
            var company = await _dbContext.Companies.FindAsync(id);

            if (company == null) throw new Exception("Company not found!");

            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
    }
}