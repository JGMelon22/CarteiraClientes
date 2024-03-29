namespace CarteiraClientes.ViewModels.Company;

public record CompanyResultViewModel
{
    public int CompanyId { get; init; }
    public string CompanyName { get; init; }
    public DateTime FoundedDate { get; init; }
    public decimal Revenue { get; init; }
}