using System.ComponentModel.DataAnnotations;
using CarteiraClientes.ViewModels.ClientCompany;

namespace CarteiraClientes.ViewModels.Client;

public record ClientInputViewModel
{
    public required string FullName { get; init; }
    public byte Age { get; init; }
    public required string Document { get; init; }

    [EnumDataType(typeof(Gender))] public Gender Gender { get; init; }

    public bool IsOverdue { get; init; }
    public List<AddClientAsyncCompanyViewModel> ClientsCompanies { get; init; } = new();
}