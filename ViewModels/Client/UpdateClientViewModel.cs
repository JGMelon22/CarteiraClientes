namespace CarteiraClientes.ViewModels.Client;

public record UpdateClientViewModel()
{
    public int ClientId { get; init; }
    public required string FullName { get; init; }
    public byte Age { get; init; }
    public required string Document { get; init; }
    public Gender Gender { get; init; }
    public bool IsOverdue { get; init; }
}