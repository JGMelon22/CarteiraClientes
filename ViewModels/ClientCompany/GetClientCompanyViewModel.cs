using System.ComponentModel.DataAnnotations;

namespace CarteiraClientes.ViewModels.ClientCompany;

public record GetClientCompanyViewModel
{
    // Dados Client
    public int ClientId { get; init; }
    public  string FullName { get; init; }
    public byte Age { get; init; }
    public  string Document { get; init; }
    [EnumDataType(typeof(Gender))] public Gender Gender { get; init; }
    public bool IsOverdue { get; init; }

    // Dados Empresa
    public int CompanyId { get; init; }
    public  string CompanyName { get; init; }
    public DateTime FoundedDate { get; init; }
    public decimal Revenue { get; init; }
}