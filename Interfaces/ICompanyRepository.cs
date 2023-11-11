using CarteiraClientes.Models;
using CarteiraClientes.ViewModels.Company;

namespace CarteiraClientes.Interfaces;

public interface ICompanyRepository
{
    Task<ServiceResponse<List<GetCompanyViewModel>>> GetAllCompanies();
    Task<ServiceResponse<GetCompanyViewModel>> GetCompanyById(int id);
    Task AddCompany(AddCompanyViewModel newCompany);
    Task<ServiceResponse<GetCompanyViewModel>> UpdateCompany(UpdateCompanyViewModel updatedCompany);
    Task RemoveCompany(int id);
}