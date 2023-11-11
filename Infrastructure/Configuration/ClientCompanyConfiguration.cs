using CarteiraClientes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteiraClientes.Infrastructure.Configuration;

public class ClientCompanyConfiguration : IEntityTypeConfiguration<ClientCompany>
{
    public void Configure(EntityTypeBuilder<ClientCompany> builder)
    {
        builder.ToTable("clients_companies");

        builder.HasKey(cc => new
        {
            cc.ClientId,
            cc.CompanyId
        });

        builder.HasIndex(cc => cc.ClientId)
            .HasDatabaseName("client_company_client_id_idx");

        builder.HasIndex(cc => cc.CompanyId)
            .HasDatabaseName("client_company_company_id_idx");

        // Explicit FK configuration 
        builder.HasOne(cc => cc.Client)
            .WithMany(c => c.ClientsCompanies)
            .HasForeignKey(cc => cc.ClientId);

        builder.HasOne(cc => cc.Company)
            .WithMany(c => c.ClientsCompanies)
            .HasForeignKey(cc => cc.CompanyId);

        builder.Property(cc => cc.ClientId)
            .HasColumnName("client_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(cc => cc.CompanyId)
            .HasColumnName("company_id")
            .ValueGeneratedOnAdd()
            .IsRequired();
    }
}