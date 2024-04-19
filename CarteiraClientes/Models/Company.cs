namespace CarteiraClientes.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public DateTime FoundedDate { get; set; }
    public decimal Revenue { get; set; }
    public List<ClientCompany> ClientsCompanies { get; set; } = null!;
}