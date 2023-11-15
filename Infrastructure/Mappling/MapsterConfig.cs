using CarteiraClientes.ViewModels.ClientCompany;
using Mapster;

namespace CarteiraClientes.Infrastructure.Mappling;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<ClientCompany, GetClientCompanyViewModel>
            .NewConfig()
            .Map(dest => dest.ClientId, src => src.Client.ClientId) // Mapeando Clientes
            .Map(dest => dest.FullName, src => src.Client.FullName)
            .Map(dest => dest.Age, src => src.Client.Age)
            .Map(dest => dest.Document, src => src.Client.Document)
            .Map(dest => dest.Gender, src => src.Client.Gender)
            .Map(dest => dest.IsOverdue, src => src.Client.IsOverdue)
            .Map(dest => dest.CompanyId, src => src.Company.CompanyId) // Mapeando Empresa
            .Map(dest => dest.CompanyName, src => src.Company.CompanyName)
            .Map(dest => dest.FoundedDate, src => src.Company.FoundedDate)
            .Map(dest => dest.Revenue, src => src.Company.Revenue);
    }
}