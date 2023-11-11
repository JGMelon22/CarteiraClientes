namespace CarteiraClientes.ViewModels.Company;

public record UpdateCompanyViewModel
{
    public int CompanyId { get; init; }
    public required string CompanyName { get; init; }
    public DateTime FoundedDate { get; init; }
    public decimal Revenue { get; init; }
}