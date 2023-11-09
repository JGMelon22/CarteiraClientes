namespace CarteiraClientes.Models;

public class ClientCompany
{
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}