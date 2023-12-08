namespace CarteiraClientes.ViewModels.Report;

public record GetReportViewModel
{
    public int ClientId { get; init; }
    public required string FullName { get; init; }
    public required string Document { get; init; }
    public bool IsOverdue { get; init; }
    public int CompanyId { get; init; }
    public required string CompanyName { get; init; }
}