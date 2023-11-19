using Bogus;
using Bogus.Extensions.Portugal;

namespace CarteiraClientes.Infrastructure.Configuration.Seeding;

public class InitialSeeding
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var client = new Faker<Client>()
            .RuleFor(c => c.ClientId, f => f.IndexFaker + 1)
            .RuleFor(c => c.FullName, f => f.Person.FullName)
            .RuleFor(c => c.Age, f => (byte)f.Random.Number(18, 60))
            .RuleFor(c => c.Document, f => f.Person.Nif()) // Número de Identificação Fiscal, pois não consta RG na lib
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.IsOverdue, f => f.Random.Bool());

        var company = new Faker<Company>()
            .RuleFor(c => c.CompanyId, f => f.IndexFaker + 1)
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.FoundedDate, f => f.Date.Past())
            .RuleFor(c => c.Revenue, f => f.Random.Decimal(1000M, 99999999999999999.99M));

        modelBuilder.Entity<Client>()
            .HasData(client.GenerateBetween(1000, 1000));

        modelBuilder.Entity<Company>()
            .HasData(company.GenerateBetween(1000, 1000));
    }
}