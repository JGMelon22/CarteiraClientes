namespace CarteiraClientes.ViewModels.Report;

public record ReportResultViewModel
{
    public int ClientId { get; init; }
    public string FullName { get; init; }
    public string Document { get; init; }
    public bool IsOverdue { get; init; }
    public int CompanyId { get; init; }
    public string CompanyName { get; init; }
}