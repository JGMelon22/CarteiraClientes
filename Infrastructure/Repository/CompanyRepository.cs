using System.Data;
using CarteiraClientes.ViewModels.Company;
using Dapper;

namespace CarteiraClientes.Infrastructure.Repository;

public class CompanyRepository : ICompanyRepository
{
    private static readonly Func<AppDbContext, int, Task<GetCompanyViewModel?>> GetCompany =
        EF.CompileAsyncQuery((AppDbContext context, int id) =>
            context.Companies
                .Select(c => new GetCompanyViewModel
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

    public async Task<ServiceResponse<List<GetCompanyViewModel>>> GetAllCompanies()
    {
        var serviceResponse = new ServiceResponse<List<GetCompanyViewModel>>();
        var getAllCompaniesQuery = """
                                   select company_id as CompanyId,
                                          company_name as CompanyName,
                                          founded_date as FoundedDate,
                                          revenue as Revenue
                                   from companies
                                   order by company_id asc
                                   limit 100;
                                   """;

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<GetCompanyViewModel>(getAllCompaniesQuery);
        serviceResponse.Data = result.ToList();

        _dbConnection.Close();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCompanyViewModel>> GetCompanyByIdCompiledEfCoreQueryAsync(int id)
    {
        var serviceResponse = new ServiceResponse<GetCompanyViewModel>();

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

    public async Task AddCompany(AddCompanyViewModel newCompany)
    {
        var company = newCompany.Adapt<Company>();

        await _dbContext.AddAsync(company);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ServiceResponse<GetCompanyViewModel>> UpdateCompany(UpdateCompanyViewModel updatedCompany)
    {
        var serviceResponse = new ServiceResponse<GetCompanyViewModel>();

        try
        {
            var company = await _dbContext.Companies.FindAsync(updatedCompany.CompanyId);

            if (company == null) throw new Exception("Company not found!");

            company.Adapt<UpdateCompanyViewModel>();

            company.CompanyName = updatedCompany.CompanyName;
            company.FoundedDate = updatedCompany.FoundedDate;
            company.Revenue = updatedCompany.Revenue;

            await _dbContext.SaveChangesAsync();

            serviceResponse.Data = company.Adapt<GetCompanyViewModel>();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task RemoveCompany(int id)
    {
        var serviceResponse = new ServiceResponse<GetCompanyViewModel>();

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