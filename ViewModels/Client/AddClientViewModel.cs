namespace CarteiraClientes.ViewModels.Client;

public record AddClientViewModel(
    string FullName,
    byte Age,
    string Document,
    Gender Gender,
    bool IsOverdue
);