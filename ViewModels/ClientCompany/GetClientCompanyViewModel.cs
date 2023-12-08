using System.ComponentModel.DataAnnotations;

namespace CarteiraClientes.ViewModels.ClientCompany;

public record GetClientCompanyViewModel
{
    // Dados Client
    public int ClientId { get; init; }
    public required string FullName { get; init; }
    public required string Document { get; init; }
    public bool IsOverdue { get; init; }

    // Dados Empresa
    public int CompanyId { get; init; }
    public required string CompanyName { get; init; }
}