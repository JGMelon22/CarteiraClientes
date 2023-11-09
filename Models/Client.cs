using CarteiraClientes.Models.Enums;

namespace CarteiraClientes.Models;

public class Client
{
    public int ClientId { get; set; }
    public required string FullName { get; set; }
    public byte Age { get; set; }
    public required string Document { get; set; } // Nesse caso seria o RG
    public Gender Gender { get; set; }
}