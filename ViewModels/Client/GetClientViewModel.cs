using CarteiraClientes.Models.Enums;

namespace CarteiraClientes.ViewModels.Client;

// Sim, C# presa pela verticalidade, diferente do Java (não é uma ofensa, mas coding conventions distintos)
public record GetClientViewModel(
    int ClientId,
    string FullName,
    byte Age,
    string Document,
    Gender Gender,
    bool IsOverdue
);