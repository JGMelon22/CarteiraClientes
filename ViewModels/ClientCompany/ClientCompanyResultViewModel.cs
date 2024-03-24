namespace CarteiraClientes.ViewModels.ClientCompany;

public record ClientCompanyResultViewModel
{
    // Dados Client
    public int ClientId { get; init; }
    public string FullName { get; init; }
    public string Document { get; init; }
    public bool IsOverdue { get; init; }

    // Dados Empresa
    public int CompanyId { get; init; }
    public string CompanyName { get; init; }
}