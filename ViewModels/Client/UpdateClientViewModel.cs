namespace CarteiraClientes.ViewModels.Client;

public record UpdateClientViewModel(
    int ClientId,
    string FullName,
    byte Age,
    string Document,
    Gender Gender,
    bool IsOverdue
);