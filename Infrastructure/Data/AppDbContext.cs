using CarteiraClientes.Infrastructure.Configuration;

namespace CarteiraClientes.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<ClientCompany> ClientsCompanies => Set<ClientCompany>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new ClientCompanyConfiguration());

        // Uncomment if need to seed data
        // InitialSeeding.Seed(modelBuilder);
    }
}