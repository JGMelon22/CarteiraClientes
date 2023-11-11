namespace CarteiraClientes.Models;

public class Company
{
    public int CompanyId { get; set; }
    public required string CompanyName { get; set; }
    public DateTime FoundedDate { get; set; }
    public Decimal Revenue { get; set; }
    public List<ClientCompany> ClientsCompanies { get; set; } 
}