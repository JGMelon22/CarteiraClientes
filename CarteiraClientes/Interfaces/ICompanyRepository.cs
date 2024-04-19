using CarteiraClientes.ViewModels.Company;

namespace CarteiraClientes.Interfaces;

public interface ICompanyRepository
{
    Task<ServiceResponse<List<CompanyResultViewModel>>> GetAllCompaniesAsync();
    Task<ServiceResponse<CompanyResultViewModel>> GetCompanyByIdCompiledEfCoreQueryAsync(int id);
    Task AddCompanyAsync(CompanyInputViewModel newCompanyInput);
    Task<ServiceResponse<CompanyResultViewModel>> UpdateCompanyAsync(int id, CompanyInputViewModel updatedCompany);
    Task RemoveCompanyAsync(int id);
}