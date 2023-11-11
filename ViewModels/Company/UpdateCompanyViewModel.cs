namespace CarteiraClientes.ViewModels.Company;

public record UpdateCompanyViewModel(
    int CompanyId,
    string CompanyName,
    DateTime FoundedDate,
    decimal Revenue
);