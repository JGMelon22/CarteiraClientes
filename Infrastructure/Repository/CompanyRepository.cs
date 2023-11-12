using System.Data;
using CarteiraClientes.Infrastructure.Data;
using CarteiraClientes.Interfaces;
using CarteiraClientes.ViewModels.Company;
using Dapper;
using Mapster;

namespace CarteiraClientes.Infrastructure.Repository;

public class CompanyRepository : ICompanyRepository
{
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
        var getAllCompaniesQuery = @"select company_id as CompanyId,
                                            company_name as CompanyName,
                                            founded_date as FoundedDate,
                                            revenue as Revenue
                                     from companies";

        _dbConnection.Open();

        var result = await _dbConnection.QueryAsync<GetCompanyViewModel>(getAllCompaniesQuery);
        serviceResponse.Data = result.Adapt<List<GetCompanyViewModel>>().ToList();

        _dbConnection.Close();

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCompanyViewModel>> GetCompanyById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCompanyViewModel>();

        try
        {
            var company = await _dbContext.Companies.FindAsync(id);

            if (company == null)
                throw new Exception("Company not found!");

            serviceResponse.Data = company.Adapt<GetCompanyViewModel>();
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

            if (company == null)
            {
                throw new Exception("Company not found!");
            }

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

            if (company == null)
            {
                throw new Exception("Company not found!");
            }

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