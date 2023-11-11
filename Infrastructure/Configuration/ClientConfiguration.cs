using CarteiraClientes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteiraClientes.Infrastructure.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(c => c.ClientId);

        builder.HasIndex(c => c.ClientId)
            .HasDatabaseName("client_id_idx")
            .IsUnique();

        builder.Property(c => c.ClientId)
            .HasColumnName("client_id")
            .ValueGeneratedOnAdd(); // Torna esta coluna do tipo identity 

        builder.Property(c => c.FullName)
            .HasColumnType("varchar")
            .HasColumnName("full_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Age)
            .HasColumnType("smallint")
            .HasColumnName("age")
            .IsRequired();

        builder.Property(c => c.Document)
            .HasColumnType("varchar")
            .HasColumnName("document")
            .HasMaxLength(9)
            .IsRequired();

        builder.Property(c => c.Gender)
            .HasColumnType("varchar")
            .HasColumnName("gender")
            .HasMaxLength(6)
            .IsRequired();

        builder.Property(c => c.IsOverdue)
            .HasColumnType("boolean")
            .HasColumnName("is_overdue");
    }
}