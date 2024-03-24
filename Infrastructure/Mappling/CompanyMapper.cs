using CarteiraClientes.ViewModels.Company;
using Riok.Mapperly.Abstractions;

namespace CarteiraClientes.Infrastructure.Mappling;

[Mapper]
public static partial class CompanyMapper
{
    public static partial CompanyResultViewModel CompanyToCompanyResultViewModel(Company company);
    public static partial Company CompanyToCompanyInputViewModel(CompanyInputViewModel company);
    public static partial void ApplyUpdate(CompanyInputViewModel companyInput, Company company);
}