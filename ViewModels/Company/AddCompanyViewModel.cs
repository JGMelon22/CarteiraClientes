namespace CarteiraClientes.ViewModels.Company;

public record AddCompanyViewModel(
    string CompanyName,
    DateTime FoundedDate,
    decimal Revenue
);