namespace CarteiraClientes.ViewModels.Company;

public record GetCompanyViewModel(
    int CompanyId,
    string CompanyName,
    DateTime FoundedDate,
    decimal Revenue
);