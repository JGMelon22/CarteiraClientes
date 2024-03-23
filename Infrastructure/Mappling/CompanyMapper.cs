using CarteiraClientes.ViewModels.Client;
using CarteiraClientes.ViewModels.Company;

namespace CarteiraClientes.Infrastructure.Mappling;

[Riok.Mapperly.Abstractions.Mapper]
public static partial class CompanyMapper
{
    public static partial ClientResultViewModel CompanyToCompanyResultViewModel(Company company);
    public static partial Client CompanyToCompanyInputViewModel(CompanyInputViewModel company);
    public static partial void ApplyUpdate(CompanyInputViewModel companyInput, Company company);
}