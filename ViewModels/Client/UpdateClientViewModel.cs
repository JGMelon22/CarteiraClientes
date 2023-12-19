using System.ComponentModel.DataAnnotations;

namespace CarteiraClientes.ViewModels.Client;

public record UpdateClientViewModel()
{
    public required string FullName { get; init; }
    public byte Age { get; init; }
    public required string Document { get; init; }

    [EnumDataType(typeof(Gender))] public Gender Gender { get; init; }

    public bool IsOverdue { get; init; }
}