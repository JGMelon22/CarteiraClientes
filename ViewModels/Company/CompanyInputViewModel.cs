namespace CarteiraClientes.ViewModels.Company;

public record CompanyInputViewModel
{
    public required string CompanyName { get; init; }
    public DateTime FoundedDate { get; init; }
    public decimal Revenue { get; init; }
}