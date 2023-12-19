using CarteiraClientes.ViewModels.Company;

namespace CarteiraClientes.Interfaces;

public interface ICompanyRepository
{
    Task<ServiceResponse<List<GetCompanyViewModel>>> GetAllCompaniesAsync();
    Task<ServiceResponse<GetCompanyViewModel>> GetCompanyByIdCompiledEfCoreQueryAsync(int id);
    Task AddCompanyAsync(AddCompanyViewModel newCompany);
    Task<ServiceResponse<GetCompanyViewModel>> UpdateCompanyAsync(int id, UpdateCompanyViewModel updatedCompany);
    Task RemoveCompanyAsync(int id);
}