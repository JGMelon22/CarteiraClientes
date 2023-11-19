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
            .RuleFor(c => c.Document, f => f.Person.Nif())
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.IsOverdue, f => f.Random.Bool());

        var company = new Faker<Company>()
            .RuleFor(c => c.CompanyId, f => f.IndexFaker + 1)
            .RuleFor(c => c.CompanyName, f => f.Company.CompanyName())
            .RuleFor(c => c.FoundedDate, f => f.Date.Past())
            .RuleFor(c => c.Revenue, f => f.Random.Decimal(1000M, 99999999999999999.99M));

        var clientCompany = new Faker<ClientCompany>()
            .RuleFor(cc => cc.ClientId, f => f.Random.Number(1, 1000))
            .RuleFor(cc => cc.CompanyId, f => f.Random.Number(1, 1000));

        modelBuilder.Entity<Client>()
            .HasData(client.GenerateBetween(1000, 1000));

        modelBuilder.Entity<Company>()
            .HasData(company.GenerateBetween(1000, 1000));

        // Populating with Many-to-Many relationship
        modelBuilder.Entity<ClientCompany>()
            .HasData(clientCompany.GenerateBetween(1000, 1000));
    }
}