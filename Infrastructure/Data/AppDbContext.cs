using CarteiraClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace CarteiraClientes.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Company> Companies => Set<Company>();
}