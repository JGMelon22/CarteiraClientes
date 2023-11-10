using CarteiraClientes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteiraClientes.Infrastructure.Configuration;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("companies");

        builder.HasKey(c => c.CompanyId);

        builder.HasIndex(c => c.CompanyId)
            .HasDatabaseName("company_id_idx")
            .IsUnique();

        builder.Property(c => c.CompanyId)
            .HasColumnName("company_id")
            .ValueGeneratedOnAdd(); // Torna esta coluna do tipo identity

        builder.Property(c => c.CompanyName)
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.FoundedDate)
            .HasColumnType("date")
            .HasColumnName("founded_date")
            .IsRequired();

        builder.Property(c => c.Revenue)
            .HasColumnType("decimal")
            .HasPrecision(20, 2)
            .HasColumnName("revenue")
            .IsRequired();
    }
}